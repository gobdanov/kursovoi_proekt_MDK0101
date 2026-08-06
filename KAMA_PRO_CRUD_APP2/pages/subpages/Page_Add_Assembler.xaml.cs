using KAMA_PRO_CRUD_APP;
using KAMA_PRO_CRUD_APP.pages;
using KAMA_PRO_CRUD_APP2.classes.contexts;
using KAMA_PRO_CRUD_APP2.classes.repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для Page_Add_Assembler.xaml
    /// </summary>
    public partial class Page_Add_Assembler : Page
    {
        public Page_Add_Assembler()
        {
            InitializeComponent();

            role_tb.Items.Add("Admin");
            role_tb.Items.Add("User");
        }

        private async void add_assembler(object sender, RoutedEventArgs e)
        {
            DBContext db = new DBContext();

            DTO.DTO_Assembler assembler = new DTO.DTO_Assembler
            {
                Name = name_tb.Text,
                Surname = surname_tb.Text,
                Lastname = lastname_tb.Text,
                Username = username_tb.Text,
                Pwd = pwd_tb.Text,
                Role = role_tb.SelectedValue.ToString()
            };

            Repository repo = new Repository();

            await repo.CreateAssemblerAsync(assembler);

            MessageBox.Show("сборщик создан!");
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblers());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
