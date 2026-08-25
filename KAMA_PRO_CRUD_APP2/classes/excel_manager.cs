using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KAMA_PRO_CRUD_APP2.classes;
using KAMA_PRO_CRUD_APP.classes.models;


namespace KAMA_PRO_CRUD_APP2.classes
{
    public class excel_manager
    {
        public List<Plan_linkto_Trailer> ReadPlanFromExcel(string from_path)
        {
            ExcelPackage.License.SetNonCommercialPersonal("вадим александрович богданов");

            List<Plan_linkto_Trailer> plan_list = new List<Plan_linkto_Trailer>();

            var file = new FileInfo(from_path);

            using (var reader = new ExcelPackage(file))
            {
                var worksheet = reader.Workbook.Worksheets[0];
                
                if(worksheet == null)
                {
                    Console.Write("пустой лист");
                }

                int columns = worksheet.Dimension.Columns;

                if(columns != 4)
                {
                    Console.Write("неверное кол-во колонок");
                }

                int rows = worksheet.Dimension.Rows;

                for (int polzunok = 1; polzunok < rows; polzunok++)
                {
                    Plan_linkto_Trailer plan = new Plan_linkto_Trailer 
                    { 
                        Plan = worksheet.Cells[polzunok,1].GetValue<string>(),
                        Trailer = worksheet.Cells[polzunok,2].GetValue<string>(),
                        VIN = worksheet.Cells[polzunok, 3].GetValue<string>(),
                        Ready = false
                    };

                    plan_list.Add(plan);
                }
            }
            return plan_list;
        }
    }
}
