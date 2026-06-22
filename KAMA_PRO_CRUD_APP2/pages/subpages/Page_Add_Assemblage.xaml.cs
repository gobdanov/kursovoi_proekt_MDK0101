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
using KAMA_PRO_CRUD_APP2.items;
using KAMA_PRO_CRUD_APP2.classes.services;
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes.contexts;

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
            foreach (var i in repository.Trailers)
            {
                trailers_cmbbx.Items.Add(i.Name);
            }

            await repository.GetAssemblersAsync();
            foreach (var i in repository.Assemblers)
            {
                assemblers_sp.Children.Add(new CheckBox { Content = i.Username });
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

                if (count >= 13)
                {
                    MessageBox.Show("слишком много прицепов!");
                    quantity_trailers.Text = "";
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
                quantity_trailers.Text = "";
                return;
            }

        }

        private async void add_assemblage(object sender, RoutedEventArgs e)
        {
            DBContext db = new DBContext();

            Services service = new Services();

            // сохраняем собираемый прицеп
            string trailer = trailers_cmbbx.SelectedValue.ToString();

            // сохраняем кол-во прицепов
            int count = Convert.ToInt32(quantity_trailers.Text);

            List<string> components = db.Component_linkto_Trailer.Where(x => x.Trailer == trailer).Select(x => x.Component).ToList();

            bool flag2 = true;

            foreach (var i in components)
            {
                // Количество компонента на складе
                int totalQuantity = db.Components.Where(x => x.Name == i).Select(x => x.Quantity).FirstOrDefault();

                // Количество компонента, необходимого для одного прицепа
                int neededPerTrailer = db.Component_linkto_Trailer.Where(x => x.Component == i).Select(x => x.Quantity).FirstOrDefault();

                // Сколько нужно для всех прицепов
                int neededTotal = neededPerTrailer * count;

                // Проверяем, хватает ли на складе
                if (totalQuantity < neededTotal)
                {
                    flag2 = false;
                    break; // Можно выйти из цикла, так как уже не хватает
                }
            }
            if (flag2)
            {
                // содержит id сборщиков для последующего добавления
                List<int> assemblers = new List<int>();
                foreach (CheckBox i in assemblers_sp.Children)
                {
                    // если checkbox выделен, то
                    if (i.IsChecked == true)
                    {
                        // сохраняем айди того сборщика, который выделен
                        Assemblers ass = db.Assemblers.Where(x => x.Username == i.Content).First();
                        assemblers.Add(ass.Id);
                    }
                }

                // сохраняем план
                string plan = plans_cmbbx.SelectedValue.ToString();

                

                //сохраняем шильды
                List<string> EAV = new List<string>();
                //если четное, то
                if (count % 2 == 0)
                {
                    foreach (item_add_assemblage_trailer i in trailers.Children)
                    {
                        EAV.Add(i.upper_vin.Text.ToString());
                        EAV.Add(i.downer_vin.Text.ToString());
                    }
                }
                else
                {
                    bool flag = false;
                    foreach (item_add_assemblage_trailer i in trailers.Children)
                    {
                        if (flag == false)
                        {
                            EAV.Add(i.downer_vin.Text.ToString());
                            flag = true;
                        }
                        else
                        {
                            EAV.Add(i.upper_vin.Text.ToString());
                            EAV.Add(i.downer_vin.Text.ToString());
                        }
                    }
                }

                for (int i = 0; i < assemblers.Count; i++)
                {
                    for (int j = 0; j < EAV.Count; j++)
                    {
                        Assemblages assemblage = new Assemblages
                        {
                            Assembler = assemblers[i],
                            VIN = EAV[j],
                            Date_ = DateOnly.FromDateTime(DateTime.Now)
                        };

                        try
                        {
                            await service.CreateAssemblage(assemblage);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                MessageBox.Show("сборка добавлена!");
            }
            else
            {
                MessageBox.Show("комплектовки не хватает!");
            }


        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblage());
        }
    }
}