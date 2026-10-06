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

        private void Execute(string pathToFile)
        {
            try
            {
                excel_manager EM = new excel_manager();
                List<Plan_linkto_Trailer> list = EM.ReadPlanFromExcel(pathToFile);

                if (list != null)
                {
                    Repository repo = new Repository();

                    repo.PostPlan(list[0].Plan, list);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка\nОшибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
