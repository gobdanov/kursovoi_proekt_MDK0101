using KAMA_PRO_CRUD_APP;
using KAMA_PRO_CRUD_APP.pages;
using KAMA_PRO_CRUD_APP2.classes.repo;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Concrete_Assemblage.xaml
    /// </summary>
    public partial class Page_Concrete_Assemblage : Page
    {
        string date;
        public Page_Concrete_Assemblage(string date)
        {
            InitializeComponent();
            this.date = date;
            this.Loaded += Page_Concrete_AssemblageLoaded;
        }

        private async void Page_Concrete_AssemblageLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            await repo.GetConcreteAssemblagesAsync(date);
            foreach (var i in repo.concrete_Assemblage)
            {
                string assemblers = "";
                foreach(var a in i.Assemblers)
                {
                    assemblers += a+" ";
                }
                parent.Children.Add(new items.item_concrete_assemblage(i.Trailer, assemblers, i.Comments, i.Plan, i.Nameplate));
            }
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblage());
        }
    }
}
