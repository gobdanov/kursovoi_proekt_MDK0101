using KAMA_PRO_CRUD_APP2;
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
    /// Логика взаимодействия для Navigate_Salary.xaml
    /// </summary>
    public partial class Navigate_Salary : Page
    {
        public Navigate_Salary(string UserFIO,string Role)
        {
            InitializeComponent();
            userFIO.Content = UserFIO;
            salary.Background = Brushes.LightGray;
            if (Role == "User")
            {
                assemblers_btn.Visibility = Visibility.Hidden;
                storage_btn.Visibility = Visibility.Hidden;
                assemblers.Visibility = Visibility.Hidden;
                storage.Visibility = Visibility.Hidden;
            }
        }

        private void goto_authorization_window(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            MainWindow1.mainWindow1.Close();
        }

        private void goto_assemblers_page(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new pages.Page_Assemblers());
            assemblers.Background = Brushes.LightGray;
            storage.Background = null;
            plans.Background = null;
            assemblage.Background = null;
            salary.Background = null;
        }

        private void goto_storage_page(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new pages.Page_Storage());
            assemblers.Background = null;
            storage.Background = Brushes.LightGray;
            plans.Background = null;
            assemblage.Background = null;
            salary.Background = null;
        }

        private void goto_plans_page(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new pages.Page_Plan(temp_variables.loginedUser.Role));
            assemblers.Background = null;
            storage.Background = null;
            plans.Background = Brushes.LightGray;
            assemblage.Background = null;
            salary.Background = null;
        }

        private void goto_assemlage_page(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new pages.Page_Assemblage());
            assemblers.Background = null;
            storage.Background = null;
            plans.Background = null;
            assemblage.Background = Brushes.LightGray;
            salary.Background = null;
        }

        private void goto_salary_page(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new pages.Page_Salary());
            assemblers.Background = null;
            storage.Background = null;
            plans.Background = null;
            assemblage.Background = null;
            salary.Background = Brushes.LightGray;
        }
    }
}
