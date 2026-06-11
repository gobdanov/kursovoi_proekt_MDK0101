using KAMA_PRO_CRUD_APP;
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
using KAMA_PRO_CRUD_APP2.pages;
using System.Windows.Shapes;
using KAMA_PRO_CRUD_APP.pages;
using KAMA_PRO_CRUD_APP2.classes.repo;

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Add_Assemblage.xaml
    /// </summary>
    public partial class Page_Add_Assemblage : Page
    {
        public Page_Add_Assemblage()
        {
            InitializeComponent();

            this.Loaded += Page_Add_Assemblage_Loaded;

        }

        public async void Page_Add_Assemblage_Loaded(object sender, RoutedEventArgs e)
        {
            Repository repository = new Repository();
            await repository.GetTrailersAsync();

            foreach(var i in repository.Trailers)
            {
                trailers_cmbbx.Items.Add(i.Name);
            }

            await repository.GetAssemblersAsync();
            foreach (var i in repository.Assemblers)
            {
                assemblers.Children.Add(new CheckBox { Content = i.Surname+" " + i.Name+" "+ i.Lastname  });
            }

            await repository.GetComponentsAsync();
            foreach (var i in repository.Components)
            {
                equipment.Children.Add(new CheckBox { Content = i.Name, IsChecked = true });
            }

            await repository.GetPlansAsync();
            foreach (var i in repository.Plans)
            {
                plans_cmbbx.Items.Add(i.Name);
            }
        }

        private void init_trailers(object sender, TextChangedEventArgs e)
        {
            trailers.Children.Clear();
            if (quantity_trailers.Text == "") return;
            try
            {
                int count = Convert.ToInt32(quantity_trailers.Text);

                if (count >= 13) {
                    MessageBox.Show("слишком много прицепов!");
                    return;
                } 
                else
                {
                    if (count % 2 == 0)
                    {
                        count /= 2;
                        for (int i = 0; i < count; i++)
                        {
                            trailers.Children.Add(new items.item_add_assemblage_trailer(false));
                        }
                    }
                    else
                    {
                        count /= 2;
                        trailers.Children.Add(new items.item_add_assemblage_trailer(true));
                        for (int i = 0; i < count; i++)
                        {
                            trailers.Children.Add(new items.item_add_assemblage_trailer(false));
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("введите корректное число!");
                return;
            }
            
        }

        private void add_assemblage(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("добавление сборки...");
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblage());
        }
    }
}
