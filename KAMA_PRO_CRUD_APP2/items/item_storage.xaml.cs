using Accessibility;
using KAMA_PRO_CRUD_APP2.classes.services;
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
using KAMA_PRO_CRUD_APP.classes.models;

namespace KAMA_PRO_CRUD_APP.items
{
    /// <summary>
    /// Логика взаимодействия для item_storage.xaml
    /// </summary>
    public partial class item_storage : UserControl
    {
        Components component;
        public item_storage()
        {
            InitializeComponent();
        }
        public item_storage(Components component)
        {
            InitializeComponent();
            this.component = component;

            name.Content = component.Name.ToString().Replace("_", "__");
            count.Content = Convert.ToString(component.Quantity);
        }

        private void minus(object sender, RoutedEventArgs e)
        {
            //скрываем иконку минуса
            minus_img.Visibility = Visibility.Hidden;
            //скрываем кнопку minus_btn
            minus_btn.Visibility = Visibility.Hidden;

            //показываем иконку submit
            minus_submit_img.Visibility = Visibility.Visible;
            //показываем кнопку подтверждения
            minus_submit_btn.Visibility = Visibility.Visible;

            //показываем текстбокс
            minus_tb.IsEnabled = true;

        }


        private async void minus_submit(object sender, RoutedEventArgs e)
        {
            

            //показываем иконку минуса
            minus_img.Visibility = Visibility.Visible;
            //показываем кнопку minus_btn
            minus_btn.Visibility = Visibility.Visible;

            //скрываем иконку submit
            minus_submit_img.Visibility = Visibility.Hidden;
            //скрываем кнопку подтверждения
            minus_submit_btn.Visibility = Visibility.Hidden;

            Services service = new Services();

            try
            {
                if(Convert.ToInt32(count.Content) + (Convert.ToInt32(minus_tb.Text)) < 0)
                {
                    System.Windows.MessageBox.Show($"Кол-во не может быть отрицательным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    await service.UpdateComponent(component.Name, (Convert.ToInt32(minus_tb.Text)));

                    count.Content = Convert.ToString(Convert.ToInt32(count.Content) + (Convert.ToInt32(minus_tb.Text)));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            minus_tb.Text = "0";
            //скрываем текстбокс
            minus_tb.IsEnabled = false;


        }

    }
}
