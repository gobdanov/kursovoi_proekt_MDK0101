using KAMA_PRO_CRUD_APP.pages;
using KAMA_PRO_CRUD_APP2.classes.repo;
using KAMA_PRO_CRUD_APP2.pages.subpages;
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

namespace KAMA_PRO_CRUD_APP.items
{
    /// <summary>
    /// Логика взаимодействия для Item_plan.xaml
    /// </summary>
    public partial class Item_plan : UserControl
    {
        public Item_plan(string city,int all_trailers, int ready_trailers, string Role)
        {
            InitializeComponent();
            city_tb.Content = city;
            all_trailers_tb.Content = "всего: "+ all_trailers;
            ready_trailers_tb.Content = "собрано: "+ready_trailers;
        }

        private void goto_plan(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Concrete_Plan(Convert.ToString(city_tb.Content)));
        }

        private void delete_plan(object sender, RoutedEventArgs e)
        {
            try
            {
                Repository repo = new Repository();
                var response = System.Windows.MessageBox.Show($"Вы действительно хотите удалить план?\nЭто действие отменить будет невозможно", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (response == MessageBoxResult.Yes)
                {
                    repo.DeletePlanByName(city_tb.Name);
                    System.Windows.MessageBox.Show($"План удалён", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show($"Произошла ошибка {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
