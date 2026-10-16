using KAMA_PRO_CRUD_APP;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP.pages;
using KAMA_PRO_CRUD_APP2.classes;
using KAMA_PRO_CRUD_APP2.classes.repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Add_Component.xaml
    /// </summary>
    public partial class Page_Add_Component : Page
    {
        public Page_Add_Component()
        {
            InitializeComponent();
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Storage());
        }

        private async void add_component(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name_tb.Text) || !int.TryParse(quantity_tb.Text, out int quantity))
            {
                MessageBox.Show("неверно введены данные!");
                return;
            }
            Repository repo = new Repository();

            Components component = new Components
            {
                Name = name_tb.Text,
                Quantity = Convert.ToInt32(quantity_tb.Text)
            };

            await repo.CreateComponent(component);

            MessageBox.Show("компонент успешно создан!");
        }
    }
}
