using KAMA_PRO_CRUD_APP;
using KAMA_PRO_CRUD_APP.pages;
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

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Add_Assembler.xaml
    /// </summary>
    public partial class Page_Add_Assembler : Page
    {
        public Page_Add_Assembler()
        {
            InitializeComponent();
        }

        private void add_assembler(object sender, RoutedEventArgs e)
        {
            
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblers());
        }
    }
}
