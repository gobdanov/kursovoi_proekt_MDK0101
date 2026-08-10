using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes;
using KAMA_PRO_CRUD_APP2.classes.repo;
using KAMA_PRO_CRUD_APP2.pages.subpages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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

namespace KAMA_PRO_CRUD_APP.pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Assemblage.xaml
    /// </summary>
    public partial class Page_Assemblage : Page
    {
        bool is_page_loaded = false;
        public Page_Assemblage()
        {
            InitializeComponent();

            this.Loaded += Page_AssemblageLoaded;
        }

        private async void Page_AssemblageLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            await repo.GetAssemblagesAsync();
            List<DateOnly> assemblages = repo.Assemblages.Select(x => x.Date_).Distinct().ToList();

            foreach(var i in assemblages)
            {
                parent_Border.Children.Add(new items.item_assemblage(i, temp_variables.loginedUser.Role) { Margin = new Thickness(0, 5, 0, 0) });
            }
            

            cmbbx_trlr.Items.Add("выбрать...");
            await repo.GetTrailersAsync();
            foreach (var i in repo.Trailers)
            {
                cmbbx_trlr.Items.Add(i.Name);
            }
            cmbbx_trlr.SelectedIndex = 0;
            is_page_loaded = true;
        }

        private void goto_Add_Assemblage(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Add_Assemblage());
        }

        private async void model_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    await UpdateAssemblages();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void model_TextChanged(object sender, TextChangedEventArgs e)
        {
            await UpdateAssemblages();
        }

        private async Task UpdateAssemblages()
        {
            parent_Border.Children.Clear();

            Date_.Text = "";

            cmbbx_trlr.SelectedValue = null;

            Repository repo = new Repository();
            await repo.GetAssemblagesAsync();

            List<Assemblages> assemblages = repo.Assemblages
                .Where(x => x.VIN == "EAV90920" + osnost.Text + "T0" + t0_.Text)
                .ToList();

            List<DateOnly> assemblages_date = assemblages.Select(x => x.Date_).ToList();

            foreach (var i in assemblages_date)
            {
                parent_Border.Children.Add(new items.item_assemblage(i, temp_variables.loginedUser.Role)
                {
                    Margin = new Thickness(0, 5, 0, 0)
                });
            }
        }

        private async void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            parent_Border.Children.Clear();

            osnost.Text = "";
            t0_.Text = "";

            cmbbx_trlr.SelectedValue = null;

            Repository repo = new Repository();
            await repo.GetAssemblagesAsync();

            DateOnly cur_date = DateOnly.FromDateTime(Convert.ToDateTime(Date_.Text));

            List<Assemblages> assemblages = repo.Assemblages
                .Where(x => x.Date_ == cur_date)
                .ToList();

            List<DateOnly> assemblages_date = assemblages.Select(x => x.Date_).Distinct().ToList();

            foreach (var i in assemblages_date)
            {
                parent_Border.Children.Add(new items.item_assemblage(i, temp_variables.loginedUser.Role)
                {
                    Margin = new Thickness(0, 5, 0, 0)
                });
            }
        }

        private async void Combobox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (is_page_loaded) 
            {
                parent_Border.Children.Clear();

                osnost.Text = "";
                t0_.Text = "";

                Date_.Text = "";

                Repository repo = new Repository();
                await repo.GetPlan_linkto_TrailerAsync();
                await repo.GetAssemblagesAsync();

                string selectedTrailer = cmbbx_trlr.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(selectedTrailer) && selectedTrailer != "выбрать...")
                {
                    List<string> VINS = repo.Plan_linkto_Trailer
                        .Where(x => x.Trailer == selectedTrailer)
                        .Select(x => x.VIN)
                        .ToList();

                    if (VINS.Any())
                    {
                        List<Assemblages> assemblages = new List<Assemblages>();

                        foreach (string VIN in VINS)
                        {
                            Assemblages assemblage = repo.Assemblages.Where(x => x.VIN == VIN).FirstOrDefault();
                            if (assemblage != null)
                            {
                                assemblages.Add(assemblage);
                            }
                        }
                        if (assemblages.Any())
                        {
                            List<DateOnly> assemblages_date = assemblages.Select(x => x.Date_).Distinct().ToList();

                            foreach (var i in assemblages_date)
                            {
                                parent_Border.Children.Add(new items.item_assemblage(i, temp_variables.loginedUser.Role)
                                {
                                    Margin = new Thickness(0, 5, 0, 0)
                                });
                            }
                        }
                    }
                }
            }
        }
    }
}
