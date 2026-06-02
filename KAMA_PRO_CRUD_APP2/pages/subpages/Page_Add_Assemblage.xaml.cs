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
            for (int i = 0; i < 10; i++)
            {
                assemblers.Children.Add(new CheckBox { Content = "вадим богданов"});
            }

            
            equipment.Children.Add(new CheckBox { Content = "дышло_чду", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "дышло_чдш", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "дышло_дду", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "рессора_тл", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "рессора_чл", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "амортизатор", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "удлинитель_у", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "удлинитель_шир", IsChecked = true });
            equipment.Children.Add(new CheckBox { Content = "проводка", IsChecked = true });
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
