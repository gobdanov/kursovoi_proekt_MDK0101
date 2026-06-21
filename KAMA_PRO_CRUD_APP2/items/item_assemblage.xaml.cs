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
using KAMA_PRO_CRUD_APP2.classes.services;

namespace KAMA_PRO_CRUD_APP.items
{
    /// <summary>
    /// Логика взаимодействия для item_assemblage.xaml
    /// </summary>
    public partial class item_assemblage : UserControl
    {
        DateOnly dateOnly;
        public item_assemblage(DateOnly date)
        {
            InitializeComponent();
            date_tb.Content = "сборка от " + date.ToString();
            dateOnly = date;
        }

        private void goto_assemblage(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Concrete_Assemblage(Convert.ToString(dateOnly)));
        }

        private void delete_assemblage(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("удаление сборки");
        }
    }
}
