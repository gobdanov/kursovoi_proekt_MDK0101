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
using System.Windows.Shapes;
using KAMA_PRO_CRUD_APP.pages;

namespace KAMA_PRO_CRUD_APP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public static MainWindow1 mainWindow1;
        public static Frame frame1_out;
        public static Frame frame2_out;

        public MainWindow1()
        {
            InitializeComponent();
            mainWindow1 = this;
            frame1_out = frame1;
            frame2_out = frame2;
            frame1.Navigate(new Navigate_Salary());
            frame2.Navigate(new Page_Salary());
        }
    }
}
