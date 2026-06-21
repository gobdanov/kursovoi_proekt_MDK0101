using KAMA_PRO_CRUD_APP;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP.pages;
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

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Concrete_Plan.xaml
    /// </summary>
    public partial class Page_Concrete_Plan : Page
    {
        string Plan;
        public Page_Concrete_Plan(string name_plan)
        {
            InitializeComponent();
            Plan = name_plan;
            plan_name.Content = name_plan;
            this.Loaded += Page_Concrete_PlanLoaded;
        }

        private async void Page_Concrete_PlanLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            await repo.GetPlan_linkto_TrailerAsync();

            List<Plan_linkto_Trailer> plan_Linkto_Trailer = new List<Plan_linkto_Trailer>(repo.Plan_linkto_Trailer.Where(x => x.Plan == Plan));
            
            foreach(var i in plan_Linkto_Trailer)
            {
                if (Convert.ToString(i.Ready) == "False")
                {
                    parent.Children.Add(new items.item_concrete_plan(i.Trailer, i.VIN, Convert.ToString(i.Ready), "-"));
                }
                else
                {
                    List<int> assemblers = new List<int>();


                    await repo.GetAssemblagesAsync();

                    foreach (var j in repo.Assemblages)
                    {
                        if (j.VIN == i.VIN)
                        {
                            assemblers.Add(Convert.ToInt32(j.Assembler));
                        }
                    }

                    string assemblers_stroke = "";
                    
                    foreach(int j in assemblers)
                    {
                        assemblers_stroke += Convert.ToString(j)+" ";
                    }

                    parent.Children.Add(new items.item_concrete_plan(i.Trailer, i.VIN, Convert.ToString(i.Ready), assemblers_stroke));
                }

            }
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Plan());
        }
    }
}
