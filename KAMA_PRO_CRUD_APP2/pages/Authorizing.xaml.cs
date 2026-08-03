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
using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2;
using KAMA_PRO_CRUD_APP2.classes;
using KAMA_PRO_CRUD_APP2.classes.repo;


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
        private async void SignIn(object sender, RoutedEventArgs e)
        {
            
            Repository repo = new Repository();

            Assemblers user = await repo.LoginUser(username_tb.Text, password_tb.Text);


            if (user != null)
            {

                MainWindow1 mainWindow1 = new MainWindow1();

                if (!string.IsNullOrEmpty(user.Role))
                {

                    temp_variables.userRole = user.Role;


                    MessageBox.Show(temp_variables.userRole);


                    mainWindow1.Show();
                    mw.Close();
                }
                else
                {
                    MessageBox.Show("ROLE - ПУСТОЙ");
                }
            }
            else
            {
                MessageBox.Show("ошибка входа");
            }
        }
    }
}
