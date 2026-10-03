using KAMA_PRO_CRUD_APP;
using KAMA_PRO_CRUD_APP.pages;
using KAMA_PRO_CRUD_APP2.classes;
using KAMA_PRO_CRUD_APP2.classes.repo;
using OfficeOpenXml;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KAMA_PRO_CRUD_APP2.pages.subpages
{
    /// <summary>
    /// Логика взаимодействия для Page_Concrete_Assemblage.xaml
    /// </summary>
    public partial class Page_Concrete_Assemblage : Page
    {
        string date;
        public Page_Concrete_Assemblage(string date)
        {
            InitializeComponent();
            this.date = date;
            this.Loaded += Page_Concrete_AssemblageLoaded;
        }

        private async void Page_Concrete_AssemblageLoaded(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            await repo.GetConcreteAssemblagesAsync(date);

            sborka_date.Content = $"сборка за {date}";
            button_generate_report.Content = $"сгенерировать отчёт за {date}";

            foreach (var i in repo.concrete_Assemblage)
            {
                string assemblers = "";
                foreach(var a in i.Assemblers)
                {
                    assemblers += a+" ";
                }

                bool flag = false;
                if(temp_variables.loginedUser.Role == "Admin")
                {
                    flag = true;
                }

                parent.Children.Add(new items.item_concrete_assemblage(i.Trailer, assemblers, i.Comments, i.Plan, i.Nameplate, flag));
            }
            if (temp_variables.loginedUser.Role == "User")
            {

            }
        }

        private void goto_back(object sender, RoutedEventArgs e)
        {
            MainWindow1.frame2_out.Navigate(new Page_Assemblage());
        }

        private async void generate(object sender, RoutedEventArgs e)
        {
            Repository repo = new Repository();
            await repo.GetConcreteAssemblagesAsync(date);
            await repo.GetAssemblersAsync(); // чтобы иметь список всех сборщиков для поиска по ID

            // диалог сохранения
            var saveDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = $"Отчёт_сборка_{date}.xlsx"
            };

            if (saveDialog.ShowDialog() != true)
                return;

            string filePath = saveDialog.FileName;

            ExcelPackage.License.SetNonCommercialPersonal("вадим богданов");

            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Сборка");

                // Заголовок
                ws.Cells[1, 1].Value = $"сборка за {date}";
                ws.Cells[1, 1, 1, 6].Merge = true;
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[1, 1].Style.Font.Size = 14;
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Шапка таблицы
                string[] headers = { "Прицеп", "VIN", "ID сборщика", "Имя", "Фамилия", "Username" };
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cells[3, i + 1];
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }

                // Данные
                int row = 4;
                foreach (var item in repo.concrete_Assemblage)
                {
                    foreach (var assemblerId in item.Assemblers)
                    {
                        var assembler = repo.Assemblers?.FirstOrDefault(a => a.Id == assemblerId);

                        ws.Cells[row, 1].Value = item.Trailer;
                        ws.Cells[row, 2].Value = item.Nameplate; // или VIN — ???
                        ws.Cells[row, 3].Value = assemblerId;
                        ws.Cells[row, 4].Value = assembler?.Name;     // ???
                        ws.Cells[row, 5].Value = assembler?.Surname;  // ???
                        ws.Cells[row, 6].Value = assembler?.Username; // ???

                        row++;
                    }
                }

                // автоширина
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                package.SaveAs(new System.IO.FileInfo(filePath));
            }

            MessageBox.Show("Отчёт сохранён!");
        }
    }
}
