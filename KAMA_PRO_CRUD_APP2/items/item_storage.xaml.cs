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
    /// Логика взаимодействия для item_storage.xaml
    /// </summary>
    public partial class item_storage : UserControl
    {
        public item_storage()
        {
            InitializeComponent();
        }
        public item_storage(string Name, int Count)
        {
            InitializeComponent();
            name.Content = Name;
            count.Content = Convert.ToString(Count);
        }
    }
}
