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

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Concrete_Plan.xaml
    /// </summary>
    public partial class Page_Concrete_Plan : Page
    {
        public Page_Concrete_Plan()
        {
            InitializeComponent();
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2012", "EAV-001", "Готов", "Иванов, Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2013", "EAV-002", "В работе", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA TRAIL 2013", "EAV-003", "Готов", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2513", "EAV-004", "Не готов", ""));
            parent.Children.Add(new items.item_concrete_plan("KAMA TRAIL 2513", "EAV-005", "Готов", "Петров, Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2515", "EAV-006", "В работе", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3015", "EAV-007", "Готов", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3015(2оси)", "EAV-008", "Готов", "Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3515", "EAV-009", "В работе", "Иванов, Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3515(2оси)", "EAV-010", "Готов", "Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3015", "EAV-011", "Готов", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3015(2оси)", "EAV-012", "В работе", "Сидоров, Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3515", "EAV-013", "Готов", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3515(2оси)", "EAV-014", "Не готов", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA WATER", "EAV-015", "Готов", "Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2012", "EAV-016", "В работе", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2013", "EAV-017", "Готов", "Сидоров, Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA TRAIL 2013", "EAV-018", "Готов", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2513", "EAV-019", "В работе", ""));
            parent.Children.Add(new items.item_concrete_plan("KAMA TRAIL 2513", "EAV-020", "Готов", "Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2515", "EAV-021", "Не готов", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3015", "EAV-022", "Готов", "Иванов, Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3015(2оси)", "EAV-023", "В работе", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3515", "EAV-024", "Готов", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3515(2оси)", "EAV-025", "Готов", "Петров, Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3015", "EAV-026", "В работе", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3015(2оси)", "EAV-027", "Готов", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3515", "EAV-028", "Не готов", "Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRIME 3515(2оси)", "EAV-029", "Готов", "Иванов, Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA WATER", "EAV-030", "В работе", ""));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2012", "EAV-031", "Готов", "Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2013", "EAV-032", "Готов", "Иванов, Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA TRAIL 2013", "EAV-033", "В работе", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2513", "EAV-034", "Готов", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA TRAIL 2513", "EAV-035", "Не готов", "Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 2515", "EAV-036", "Готов", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3015", "EAV-037", "В работе", "Иванов, Петров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3015(2оси)", "EAV-038", "Готов", "Сидоров"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3515", "EAV-039", "Готов", "Иванов"));
            parent.Children.Add(new items.item_concrete_plan("KAMA PRO 3515(2оси)", "EAV-040", "В работе", "Петров, Сидоров"));
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Plan());
        }
    }
}
