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
using KAMA_PRO_CRUD_APP2.classes.repo;

namespace KAMA_PRO_CRUD_APP.pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Assemblers.xaml
    /// </summary>
    public partial class Page_Assemblers : Page
    {
        public Page_Assemblers()
        {
            InitializeComponent();

            this.Loaded += Page_Assemblers_Loaded;
        }

        private async void Page_Assemblers_Loaded(object sender, RoutedEventArgs e)
        {
            Repository repository = new Repository();
            await repository.GetAssemblersAsync();

            foreach (var i in repository.Assemblers)
            {
                stackpanel.Children.Add(new items.item_assembler(i.Name+" " + i.Lastname+" " + i.Surname, i.Username) { Margin = new Thickness(0, 10, 0, 0) });
            }
        }

        private void add_assembler(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Add_Assembler());
        }
    }
}
