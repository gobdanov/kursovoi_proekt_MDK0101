using KAMA_PRO_CRUD_APP.classes.models;
using KAMA_PRO_CRUD_APP2.classes.repo;
using KAMA_PRO_CRUD_APP2.classes.services;
using KAMA_PRO_CRUD_APP2.DTO;
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
using System.Xml.Linq;

namespace KAMA_PRO_CRUD_APP.items
{
    /// <summary>
    /// Логика взаимодействия для item_assembler.xaml
    /// </summary>
    public partial class item_assembler : UserControl
    {
        public string username_to_delete;

        Assemblers this_assembler;
        public item_assembler(Assemblers assembler)
        {
            InitializeComponent();

            this_assembler = assembler;

            role_edit.Items.Add("User");
            role_edit.Items.Add("Admin");

            fullname_tb.Content = $"{assembler.Surname} {assembler.Name} {assembler.Lastname}";
            fullname_edit.Text = $"{assembler.Surname} {assembler.Name} {assembler.Lastname}";

            username_tb.Content = "" + assembler.Username;
            username_edit.Text = assembler.Username;

            role_tb.Content = "" + assembler.Role;
            role_edit.SelectedValue = assembler.Role;

            id.Content = assembler.Id;

            username_to_delete = assembler.Username;
        }

        private void delete_user(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите удалить пользователя?\nОтменить это действие будет невозможно", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                Services service = new Services();
                service.DeleteAssembler(username_to_delete);
                MessageBox.Show("Пользователь удалён", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void edit_user(object sender, RoutedEventArgs e)
        {
            fullname_edit.Visibility = Visibility.Visible;
            username_edit.Visibility = Visibility.Visible;
            role_edit.Visibility = Visibility.Visible;

            pencil.Visibility = Visibility.Hidden;
            confirm.Visibility = Visibility.Visible;

            fullname_tb.Content = null;
            username_tb.Content = null;

        }

        private async void confirm_btn(object sender, RoutedEventArgs e)
        {
            if (role_edit.SelectedValue == null
                || string.IsNullOrEmpty(username_edit.Text)
                || string.IsNullOrEmpty(fullname_edit.Text))
            {
                System.Windows.MessageBox.Show($"Введены не все данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    Repository repo = new Repository();

                    DTO_Assembler new_assembler = new DTO_Assembler();
                    new_assembler.Pwd = this_assembler.Pwd;

                    new_assembler.Role = role_edit.SelectedValue?.ToString();
                    role_tb.Content = role_edit.SelectedValue.ToString();

                    new_assembler.Username = username_edit.Text;
                    username_tb.Content = username_edit.Text;

                    string[] FIO = fullname_edit.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    new_assembler.Surname = FIO[0];
                    new_assembler.Name = FIO[1];
                    new_assembler.Lastname = FIO[2];

                    fullname_tb.Content = fullname_edit.Text;

                    await repo.UpdateAssembler(this_assembler.Id, new_assembler);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                fullname_edit.Visibility = Visibility.Hidden;
                username_edit.Visibility = Visibility.Hidden;
                role_edit.Visibility = Visibility.Hidden;

                pencil.Visibility = Visibility.Visible;
                confirm.Visibility = Visibility.Hidden;
            }
        }
    }
}
