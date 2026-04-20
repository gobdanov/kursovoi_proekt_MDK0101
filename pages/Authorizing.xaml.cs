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
using KAMA_PRO_CRUD_APP;


namespace KAMA_PRO_CRUD_APP.pages
{
    /// <summary>
    /// Логика взаимодействия для Authorizing.xaml
    /// </summary>
    public partial class Authorizing : Page
    {
        MainWindow mw;
        public Authorizing(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }
        private void SignIn(object sender, RoutedEventArgs e)
        {
            MainWindow1 mainWindow1 = new MainWindow1();
            mainWindow1.Show();
            mw.Close();
        }
    }
}
