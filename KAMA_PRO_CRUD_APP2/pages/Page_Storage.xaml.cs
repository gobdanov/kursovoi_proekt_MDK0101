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
using KAMA_PRO_CRUD_APP2.classes.repo;

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
        
            foreach(var i in repo.Components)
            {
                parent.Children.Add(new items.item_storage($"{i.Name}", i.Quantity));
            }
            
        }
        
    }
}
