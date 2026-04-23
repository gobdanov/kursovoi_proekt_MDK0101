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
    /// Логика взаимодействия для Page_Assemblers.xaml
    /// </summary>
    public partial class Page_Assemblers : Page
    {
        public Page_Assemblers()
        {
            InitializeComponent();
            stackpanel.Children.Add(new items.item_assembler { Margin = new Thickness(0, 10, 0, 0) });
            stackpanel.Children.Add(new items.item_assembler { Margin = new Thickness(0, 10, 0, 0) });
            stackpanel.Children.Add(new items.item_assembler { Margin = new Thickness(0, 10, 0, 0) });
            stackpanel.Children.Add(new items.item_assembler { Margin = new Thickness(0, 10, 0, 0) });
        }
    }
}
