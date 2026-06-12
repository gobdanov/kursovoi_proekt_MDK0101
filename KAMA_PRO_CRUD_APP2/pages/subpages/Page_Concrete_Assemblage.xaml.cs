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
    /// Логика взаимодействия для Page_Concrete_Assemblage.xaml
    /// </summary>
    public partial class Page_Concrete_Assemblage : Page
    {
        public Page_Concrete_Assemblage()
        {
            InitializeComponent();
            parent.Children.Add(new items.item_concrete_assemblage("аа", "аа", "аа", "аа", "аа", "аа"));
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblage());
        }
    }
}
