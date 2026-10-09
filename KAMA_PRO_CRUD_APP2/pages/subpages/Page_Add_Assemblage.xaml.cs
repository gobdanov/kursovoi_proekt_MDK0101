using KAMA_PRO_CRUD_APP;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP.items;
using KAMA_PRO_CRUD_APP.pages;
using KAMA_PRO_CRUD_APP2.classes.contexts;
using KAMA_PRO_CRUD_APP2.classes.repo;
using KAMA_PRO_CRUD_APP2.classes.services;
using KAMA_PRO_CRUD_APP2.DTO;
using KAMA_PRO_CRUD_APP2.items;
using KAMA_PRO_CRUD_APP2.pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Add_Assemblage.xaml
    /// </summary>
    public partial class Page_Add_Assemblage : Page
    {
        Repository repo = new Repository();
        public Page_Add_Assemblage()
        {
            InitializeComponent();

            this.Loaded += Page_Add_Assemblage_Loaded;
        }


        public async void Page_Add_Assemblage_Loaded(object sender, RoutedEventArgs e)
        {


            await repo.GetTrailersAsync();
            foreach (var i in repo.Trailers)
            {
                trailers_cmbbx.Items.Add(i.Name);
            }

            await repo.GetAssemblersAsync();
            foreach (var i in repo.Assemblers)
            {
                assemblers_sp.Children.Add(new CheckBox { Content = i.Username });
            }

            await repo.GetPlansAsync();
            foreach (var i in repo.Plans)
            {
                plans_cmbbx.Items.Add(i.Name);
            }
        }

        private void init_trailers(object sender, TextChangedEventArgs e)
        {
            trailers.Children.Clear();
            if (quantity_trailers.Text == "") return;
            try
            {
                int count = Convert.ToInt32(quantity_trailers.Text);

                if (count >= 13)
                {
                    MessageBox.Show("Слишком много прицепов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    quantity_trailers.Text = "";
                    return;
                }
                else
                {
                    if (count % 2 == 0)
                    {
                        count /= 2;
                        for (int i = 0; i < count; i++)
                        {
                            trailers.Children.Add(new items.item_add_assemblage_trailer(false));
                        }
                    }
                    else
                    {
                        count /= 2;
                        trailers.Children.Add(new items.item_add_assemblage_trailer(true));
                        for (int i = 0; i < count; i++)
                        {
                            trailers.Children.Add(new items.item_add_assemblage_trailer(false));
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Введите корректное число", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                quantity_trailers.Text = "";
                return;
            }

        }

        private static readonly Regex VinRegex = new Regex(@"^EAV90920[13]T\d{4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private async void add_assemblage(object sender, RoutedEventArgs e)
        {
            // ---------- UI-валидация ----------
            if (trailers_cmbbx.SelectedValue == null)
            {
                MessageBox.Show("Не выбрана модель прицепа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (plans_cmbbx.SelectedValue == null)
            {
                MessageBox.Show("Не выбран план", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(quantity_trailers.Text))
            {
                MessageBox.Show("Не введено кол-во прицепов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!assemblers_sp.Children.OfType<CheckBox>().Any(cb => cb.IsChecked == true))
            {
                MessageBox.Show("Не выбран ни один сборщик", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ---------- Проверка VIN-полей ----------
            bool hasError = false;
            foreach (item_add_assemblage_trailer i in trailers.Children)
            {
                bool needUpper = !i.is_even;
                if (needUpper)
                {
                    if (string.IsNullOrEmpty(i.downer_vin.Text) ||
                        string.IsNullOrEmpty(i.upper_vin.Text) ||
                        !VinRegex.IsMatch(i.downer_vin.Text) ||
                        !VinRegex.IsMatch(i.upper_vin.Text))
                    {
                        hasError = true;
                        break;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(i.downer_vin.Text) ||
                        !VinRegex.IsMatch(i.downer_vin.Text))
                    {
                        hasError = true;
                        break;
                    }
                }
            }

            if (hasError)
            {
                MessageBox.Show("Неверно введены VIN-коды", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ---------- Сбор данных для отправки ----------
            string trailer = trailers_cmbbx.SelectedValue.ToString();
            string plan = plans_cmbbx.SelectedValue.ToString();
            int count = Convert.ToInt32(quantity_trailers.Text);

            // Сбор VIN-ов с учётом чётности
            List<string> EAV = new List<string>();
            if (count % 2 == 0)
            {
                foreach (item_add_assemblage_trailer i in trailers.Children)
                {
                    EAV.Add(i.upper_vin.Text);
                    EAV.Add(i.downer_vin.Text);
                }
            }
            else
            {
                bool first = true;
                foreach (item_add_assemblage_trailer i in trailers.Children)
                {
                    if (first)
                    {
                        EAV.Add(i.downer_vin.Text);
                        first = false;
                    }
                    else
                    {
                        EAV.Add(i.upper_vin.Text);
                        EAV.Add(i.downer_vin.Text);
                    }
                }
            }

            var duplicates = EAV
                .GroupBy(v => v, StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicates.Count > 0)
            {
                MessageBox.Show(
                    "Обнаружены дублирующиеся VIN-коды:\n" + string.Join("\n", duplicates),
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            // Сбор Id сборщиков
            List<int> assemblerIds = new List<int>();
            foreach (CheckBox cb in assemblers_sp.Children.OfType<CheckBox>())
            {
                if (cb.IsChecked == true)
                {
                    var ass = repo.Assemblers.Where(x => x.Username == cb.Content?.ToString()).FirstOrDefault();
                    if (ass != null)
                    {
                        assemblerIds.Add(ass.Id);
                    }
                }
            }

            if (assemblerIds.Count == 0)
            {
                MessageBox.Show("Не удалось определить выбранных сборщиков", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // ---------- Формирование DTO ----------
            DTO_AddAssemblage request = new DTO_AddAssemblage
            {
                Plan = plan,
                Trailer = trailer,
                VINs = EAV,
                AssemblerIds = assemblerIds,
                Date = DateOnly.FromDateTime(DateTime.Now)
            };

            // ---------- Отправка на сервер ----------
            string error = await repo.CreateAssemblagesAsync(request);

            if (error != null)
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Сборка добавлена", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblage());
        }
    }
}