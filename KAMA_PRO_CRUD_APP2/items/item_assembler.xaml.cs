using KAMA_PRO_CRUD_APP2.classes.services;
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
using System.Xml.Linq;

namespace KAMA_PRO_CRUD_APP.items
{
    /// <summary>
    /// Логика взаимодействия для item_assembler.xaml
    /// </summary>
    public partial class item_assembler : UserControl
    {
        public string username_to_delete;
        public item_assembler(string fullname, string username)
        {
            InitializeComponent();
            fullname_tb.Content = fullname;
            username_tb.Content = username;

            username_to_delete = username;
        }

        private void delete_user(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("вы действительно хотите удалить пользователя? отменить это действие будет невозможно", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {

                Services service = new Services();
                service.DeleteAssembler(username_to_delete);
                MessageBox.Show("удаление пользователя...");
            }
        }

        private void edit_user(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("изменение данных пользователя...");
        }
    }
}
