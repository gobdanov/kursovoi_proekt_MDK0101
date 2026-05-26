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
            parent.Children.Add(new items.item_storage("дышло__чду", 0));
            parent.Children.Add(new items.item_storage("дышло__чдш", 0));
            parent.Children.Add(new items.item_storage("дышло__дду", 0));
            parent.Children.Add(new items.item_storage("рессора__тл", 0));
            parent.Children.Add(new items.item_storage("рессора__чл", 0));
            parent.Children.Add(new items.item_storage("амортизатор", 0));
            parent.Children.Add(new items.item_storage("удлинитель__у", 0));
            parent.Children.Add(new items.item_storage("удлинитель__ш", 0));
            parent.Children.Add(new items.item_storage("проводка", 0));
        }
    }
}
