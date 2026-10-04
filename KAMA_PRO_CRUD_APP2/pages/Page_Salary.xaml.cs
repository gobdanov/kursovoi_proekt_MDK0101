using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes;
using KAMA_PRO_CRUD_APP2.classes.DTO;
using KAMA_PRO_CRUD_APP2.classes.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using static KAMA_PRO_CRUD_APP2.classes.repo.Repository;

namespace KAMA_PRO_CRUD_APP.pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Salary.xaml
    /// </summary>
    public partial class Page_Salary : Page
    {
        public Page_Salary()
        {
            InitializeComponent();
            salaryFor.Content = "зарплата для пользователя " + temp_variables.loginedUser.Username;
            DateTime[] days = getWeek();
            salaryAt.Content = $"за период {days[0].ToString().Split()[0]} - {days[1].ToString().Split()[0]}";

            this.Loaded += Page_SalaryLoaded;
        }

        private async void Page_SalaryLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();

            DTORETURN DTO = await repo.GetSalary(temp_variables.loginedUser.Id);

            if (DTO.ALLSUM != null && DTO.MATRIX != null)
            {

                foreach (helpful_class i in DTO.MATRIX)
                {
                    sdelka.Children.Add(new Label { Content = i.Trailer_Vin + " | " + i.count + "р" });
                }
            }

            List<DTO_Payment> payments = await repo.GetPayment(temp_variables.loginedUser.Id);
            if (payments != null)
            {
                int sum = 0;
                foreach (var pay in payments)
                {
                    if (pay.Date_ > DateOnly.FromDateTime(getWeek()[0]) && pay.Date_ < DateOnly.FromDateTime(getWeek()[1]))
                    {
                        sum += pay.Hours * 350;
                    }
                }
                Payment.Content = sum + "р";

                int trulyAllSum = DTO.ALLSUM + sum;

                allSum.Content = trulyAllSum + "р";
            }

            await repo.GetAssemblersAsync();
            foreach (var i in repo.Assemblers)
            {
                usersCombobox.Items.Add($"id:{i.Id}: {i.Surname} {i.Name} {i.Lastname}");
            }

            if (temp_variables.loginedUser.Role != "Admin")
            {
                post_salary_div.Visibility = Visibility.Hidden;
            }
        }


        public static DateTime[] getWeek()
        {
            DateTime[] first_and_last = new DateTime[2];

            DayOfWeek DayOfWeek = DateTime.Now.DayOfWeek;
            if (DayOfWeek == DayOfWeek.Monday)
            {
                first_and_last[0] = DateTime.Now.Date;
                first_and_last[1] = DateTime.Now.Date.AddDays(6);
            }
            else if (DayOfWeek == DayOfWeek.Tuesday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-1);
                first_and_last[1] = DateTime.Now.Date.AddDays(5);
            }
            else if (DayOfWeek == DayOfWeek.Wednesday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-2);
                first_and_last[1] = DateTime.Now.Date.AddDays(5);
            }
            else if (DayOfWeek == DayOfWeek.Thursday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-3);
                first_and_last[1] = DateTime.Now.Date.AddDays(4);
            }
            else if (DayOfWeek == DayOfWeek.Friday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-4);
                first_and_last[1] = DateTime.Now.Date.AddDays(3);
            }
            else if (DayOfWeek == DayOfWeek.Saturday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-5);
                first_and_last[1] = DateTime.Now.Date.AddDays(2);
            }
            else if (DayOfWeek == DayOfWeek.Sunday)
            {
                first_and_last[0] = DateTime.Now.Date.AddDays(-6);
                first_and_last[1] = DateTime.Now.Date.AddDays(1);
            }
            return first_and_last;
        }

        private async void createPayment(object sender, RoutedEventArgs e)
        {
            try
            {
                if (usersCombobox.SelectedValue == null |
                    string.IsNullOrWhiteSpace(countHours.Text))
                {     
                    MessageBox.Show("Введены не все данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int assemblerId = Convert.ToInt32(usersCombobox.SelectedValue
                    .ToString()
                    .Split()[0]
                    .Replace("id", "")
                    .Replace(":", ""));

                    int hours = Convert.ToInt32(countHours.Text);

                    Repository repo = new Repository();

                    DTO_Payment dtoPayment = new DTO_Payment
                    {
                        Assembler = Convert.ToInt32(assemblerId),
                        Date_ = DateOnly.FromDateTime(DateTime.Now),
                        Hours = hours
                    };

                    await repo.CreatePayment(dtoPayment);

                    MessageBox.Show("Часы успешно назначены сборщику", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введен неверный тип данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Введеные данные слишком велики", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Неизвестная ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
