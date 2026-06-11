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
    /// Логика взаимодействия для Page_Assemblage.xaml
    /// </summary>
    public partial class Page_Assemblage : Page
    {
        public Page_Assemblage()
        {
            InitializeComponent();

            this.Loaded += Page_AssemblageLoaded;
        }

        private async void Page_AssemblageLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            await repo.GetAssemblagesAsync();
            foreach(var i in repo.Assemblages)
            {
                parent_Border.Children.Add(new items.item_assemblage(i.Date_) { Margin = new Thickness(0, 5, 0, 0) });
            }
            

            cmbbx_trlr.Items.Add("выбрать...");
            await repo.GetTrailersAsync();
            foreach (var i in repo.Trailers)
            {
                cmbbx_trlr.Items.Add(i.Name);
            }
            cmbbx_trlr.SelectedIndex = 0;
        }

        private void goto_Add_Assemblage(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Add_Assemblage());
        }
    }
}
