using KAMA_PRO_CRUD_APP.classes.models;
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

namespace KAMA_PRO_CRUD_APP.pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Storage.xaml
    /// </summary>
    public partial class Page_Storage : Page
    {
        public Page_Storage()
        {
            InitializeComponent();
            this.Loaded += Page_StorageLoaded;
            
        }
        private async void Page_StorageLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            await repo.GetComponentsAsync();
        
            foreach(Components i in repo.Components)
            {
                parent.Children.Add(new items.item_storage(i));
            }
        }

        private void goto_page_add_comp(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Add_Component());
        }
    }
}
