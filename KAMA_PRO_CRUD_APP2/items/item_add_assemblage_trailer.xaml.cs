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

namespace KAMA_PRO_CRUD_APP2.items
{
    /// <summary>
    /// Логика взаимодействия для item_add_assemblage_trailer.xaml
    /// </summary>
    public partial class item_add_assemblage_trailer : UserControl
    {
        public item_add_assemblage_trailer()
        {
            InitializeComponent();
        }
        public item_add_assemblage_trailer(bool is_even)
        {
            InitializeComponent();
            if(is_even)
                upper.Visibility = Visibility.Hidden;
        }

        private void goto_add_photo_trailer(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("добавление фото для сборки...");
        }
    }
}
