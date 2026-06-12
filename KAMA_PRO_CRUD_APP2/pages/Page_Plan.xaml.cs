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
    /// Логика взаимодействия для Page_Plan.xaml
    /// </summary>
    public partial class Page_Plan : Page
    {
        public Page_Plan()
        {
            InitializeComponent();
            this.Loaded += Page_PlanLoaded;
            
        }
        private async void Page_PlanLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();

            await repo.GetPlansAsync();
            await repo.GetPlan_linkto_TrailerAsync();

            foreach (var i in repo.Plans)
            {
                int all_trailers = repo.Plan_linkto_Trailer.Where(x => x.Plan == i.Name).Select(x=> x.Ready).Count();
                int ready_trailers = repo.Plan_linkto_Trailer.Where(x => x.Plan == i.Name && x.Ready == true).Select(x => x.Ready).Count();
                parent_Border.Children.Add(new items.Item_plan(i.Name,all_trailers,ready_trailers));
            }
        }
        private void add_plan(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Add_Plan());
        }
    }
}
