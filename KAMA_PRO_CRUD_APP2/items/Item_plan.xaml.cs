using KAMA_PRO_CRUD_APP.pages;
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
            if (Role == "User")
            {
                delete_plan_btn.Visibility = Visibility.Hidden;
                delete_plan_img.Visibility = Visibility.Hidden;
            }
        }

        private void goto_plan(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Concrete_Plan(Convert.ToString(city_tb.Content)));
        }

        private void delete_plan(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("удаление плана");
        }
    }
}
