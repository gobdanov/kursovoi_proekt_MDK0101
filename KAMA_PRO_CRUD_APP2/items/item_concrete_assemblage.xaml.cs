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
    /// Логика взаимодействия для item_concrete_assemblage.xaml
    /// </summary>
    public partial class item_concrete_assemblage : UserControl
    {
        public item_concrete_assemblage(
            string trailer,
            string assemblers,
            string comments,
            string plan,
            string nameplate,
            string photo)
        {
            InitializeComponent();
            Trailer.Content = trailer;
            Assemblers.Content = assemblers;
            Comments.Content = comments;
            Plan.Content = plan;
            Nameplate.Content = nameplate;
        }
    }
}
