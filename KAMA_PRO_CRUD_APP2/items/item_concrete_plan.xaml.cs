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

namespace KAMA_PRO_CRUD_APP2.items
{
    /// <summary>
    /// Логика взаимодействия для item_concrete_plan.xaml
    /// </summary>
    public partial class item_concrete_plan : UserControl
    {
        public item_concrete_plan()
        {
            InitializeComponent();
        }
        public item_concrete_plan(string name, string eav, string is_ready, string assemblers)
        {
            InitializeComponent();
            Name.Content = name.ToString();
            Nameplate.Content = eav.ToString();
            Is_Ready.Content = is_ready.ToString();
            Assemblers.Content = assemblers.ToString();
        }
    }
}
