using KAMA_PRO_CRUD_APP2.classes;
using KAMA_PRO_CRUD_APP2.classes.repo;
using KAMA_PRO_CRUD_APP2.pages.subpages;
using Microsoft.Win32;
using OfficeOpenXml;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using KAMA_PRO_CRUD_APP.classes;
using KAMA_PRO_CRUD_APP.classes.models;

namespace KAMA_PRO_CRUD_APP.pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Plan.xaml
    /// </summary>
    public partial class Page_Plan : Page
    {
        public Page_Plan(string Role)
        {
            InitializeComponent();

            if (Role == "User")
            {
                add_plan_btn.Visibility = Visibility.Hidden;
            }

            this.Loaded += Page_PlanLoaded;

        }
        private async void Page_PlanLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();

            await repo.GetPlansAsync();
            await repo.GetPlan_linkto_TrailerAsync();
            List<(string name, float koef)> matrix = new List<(string name, float koef)>();

            DateTime now = DateTime.Now;
            foreach (var i in repo.Plans)
            {
                int all_trailers = repo.Plan_linkto_Trailer.Where(x => x.Plan == i.Name).Select(x => x.Ready).Count();
                int ready_trailers = repo.Plan_linkto_Trailer.Where(x => x.Plan == i.Name && x.Ready == true).Select(x => x.Ready).Count();
                parent_Border.Children.Add(new items.Item_plan(i.Name, all_trailers, ready_trailers, temp_variables.loginedUser.Role));

                //высчитываем значения для расчёта приоритетности
                matrix.Add((i.Name, (all_trailers - ready_trailers) / (now.Date - i.created_at.Date).Days));
            }

            var sorted = matrix.OrderBy(x => x.koef).ToList();
            int count = 1;
            foreach (var i in sorted)
            {
                list_plans.Children.Add(new System.Windows.Controls.Label { Content = $"{count}. {i.name}" });
                count++;
            }
        }




        private void add_plan(object sender, RoutedEventArgs e)
        {
            ChooseExcelFile();


            //MainWindow1.frame2_out.Navigate(new Page_Add_Plan());
        }

        private void ChooseExcelFile()
        {
            try
            {
                // Создаем диалог выбора файла
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                // Настройка фильтров файлов
                dialog.Filter = "Excel файлы (*.xlsx)|*.xlsx|Все файлы (*.*)|*.*";
                dialog.FilterIndex = 1; // По умолчанию выбираем первый фильтр
                dialog.Title = "Выберите Excel файл";

                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string pathToFile = dialog.FileName;

                    Execute(pathToFile);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка\nОшибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Execute(string pathToFile)
        {
            try
            {
                excel_manager EM = new excel_manager();
                List<Plan_linkto_Trailer> list = EM.ReadPlanFromExcel(pathToFile);

                if (list == null || list.Count == 0)
                {
                    System.Windows.MessageBox.Show("Файл пуст или не удалось прочитать данные.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // --- Проверка 1: имя файла без .xlsx совпадает со значениями первого столбца ---
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pathToFile);

                var planNames = list
                    .Select(x => x.Plan)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .ToList();

                if (planNames.Count == 0)
                {
                    System.Windows.MessageBox.Show("В первом столбце нет ни одного заполненного значения.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (planNames.Count > 1)
                {
                    System.Windows.MessageBox.Show(
                        "Все строки должны относиться к одному плану.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string planNameFromFile = planNames[0];

                if (!string.Equals(fileNameWithoutExt, planNameFromFile, StringComparison.OrdinalIgnoreCase))
                {
                    var result = System.Windows.MessageBox.Show(
                        $"Имя файла не совпадает с названием плана в таблице",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                    if (result != MessageBoxResult.Yes)
                        return;
                }

                var nameRegex = new System.Text.RegularExpressions.Regex(
                    @"^[A-Za-z]{3}-\d{4}-\d{2}-\d{2}$",
                    System.Text.RegularExpressions.RegexOptions.Compiled);

                if (!nameRegex.IsMatch(fileNameWithoutExt))
                {
                    System.Windows.MessageBox.Show(
                        $"Имя файла не соответствует шаблону xxx-tttt-qq-ss\n",
                        "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var errors = new List<string>();

                for (int i = 0; i < list.Count; i++)
                {
                    var row = list[i];
                    int rowNumber = i + 2; // +2, т.к. первая строка — заголовок

                    if (string.IsNullOrWhiteSpace(row.Plan))
                        errors.Add($"Строка {rowNumber}: пустое поле «Plan»");

                    if (string.IsNullOrWhiteSpace(row.Trailer))
                        errors.Add($"Строка {rowNumber}: пустое поле «Trailer»");

                    if (string.IsNullOrWhiteSpace(row.VIN))
                    {
                        errors.Add($"Строка {rowNumber}: пустой VIN");
                    }
                }

                if (errors.Count > 0)
                {
                    int maxShow = 20;
                    string preview = string.Join("\n", errors.Take(maxShow));
                    if (errors.Count > maxShow)
                        preview += $"\n... и ещё {errors.Count - maxShow} ошибок";

                    System.Windows.MessageBox.Show(
                        $"Найдены ошибки ({errors.Count}):\n\n{preview}\n\n" +
                        "Исправьте файл и попробуйте снова.",
                        "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // --- Проверка 4: дубликаты VIN внутри файла ---
                var duplicateInFile = list
                    .GroupBy(x => x.VIN.Trim(), StringComparer.OrdinalIgnoreCase)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicateInFile.Count > 0)
                {
                    System.Windows.MessageBox.Show(
                        "В файле найдены повторяющиеся VIN:\n\n" +
                        string.Join("\n", duplicateInFile.Take(20)) +
                        (duplicateInFile.Count > 20 ? $"\n... и ещё {duplicateInFile.Count - 20}" : ""),
                        "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // --- Проверка 5: VIN уже существуют на сервере ---
                Repository repo = new Repository();

                await repo.GetPlan_linkto_TrailerAsync();

                var existingVins = new HashSet<string>(
                    repo.Plan_linkto_Trailer?
                        .Select(x => x.VIN?.Trim())
                        .Where(v => !string.IsNullOrWhiteSpace(v))
                        ?? Enumerable.Empty<string>(),
                    StringComparer.OrdinalIgnoreCase);

                var alreadyExists = list
                    .Select(x => x.VIN.Trim())
                    .Where(v => existingVins.Contains(v))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (alreadyExists.Count > 0)
                {
                    System.Windows.MessageBox.Show(
                        "Следующие VIN уже есть в базе:\n\n" +
                        string.Join("\n", alreadyExists.Take(20)) +
                        (alreadyExists.Count > 20 ? $"\n... и ещё {alreadyExists.Count - 20}" : "") +
                        "\n\nЗагрузка отменена.",
                        "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // --- Всё ок, отправляем на сервер ---
                await repo.PostPlan(planNameFromFile, list);

                parent_Border.Children.Add(new items.Item_plan(planNameFromFile, list.Count, 0, temp_variables.loginedUser.Role));

                System.Windows.MessageBox.Show($"План загружен",
                    "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка\nОшибка: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
