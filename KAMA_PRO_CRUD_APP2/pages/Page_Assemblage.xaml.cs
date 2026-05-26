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

            parent_Border.Children.Add(new items.item_assemblage { Margin = new Thickness(0, 5, 0, 0) });
            parent_Border.Children.Add(new items.item_assemblage { Margin = new Thickness(0, 5, 0, 0) });
            parent_Border.Children.Add(new items.item_assemblage { Margin = new Thickness(0, 5, 0, 0) });
            parent_Border.Children.Add(new items.item_assemblage { Margin = new Thickness(0, 5, 0, 0) });

            cmbbx_trlr.Items.Add("выбрать...");
            cmbbx_trlr.Items.Add("KAMA PRO 2012");
            cmbbx_trlr.Items.Add("KAMA PRO 2013");
            cmbbx_trlr.Items.Add("KAMA TRAIL 2013");
            cmbbx_trlr.Items.Add("KAMA PRO 2513");
            cmbbx_trlr.Items.Add("KAMA TRAIL 2513");
            cmbbx_trlr.Items.Add("KAMA PRO 2515");
            cmbbx_trlr.Items.Add("KAMA PRO 3015");
            cmbbx_trlr.Items.Add("KAMA PRO 3015(2оси)");
            cmbbx_trlr.Items.Add("KAMA PRO 3515");
            cmbbx_trlr.Items.Add("KAMA PRO 3515(2оси)");
            cmbbx_trlr.Items.Add("KAMA PRIME 3015");
            cmbbx_trlr.Items.Add("KAMA PRIME 3015(2оси)");
            cmbbx_trlr.Items.Add("KAMA PRIME 3515");
            cmbbx_trlr.Items.Add("KAMA PRIME 3515(2оси");
            cmbbx_trlr.Items.Add("KAMA WATER");
            cmbbx_trlr.SelectedIndex = 0;


        }
    }
}
