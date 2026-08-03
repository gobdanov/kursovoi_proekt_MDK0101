using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes;
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
    }
}
