
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace Interop_Excel
{
    class Program
    {
        static void Main(string[] args)
        {
            var excelApp = new Excel.Application
            {
                Visible = false
            };

            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(@"C:\Temp\ErrorsOutput.xlsx");

            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

            Excel.Range excelRange = excelWorksheet.UsedRange;

            var errorsList = new List<ErrorModel>();

            for (int i = 2; i <= excelRange.Rows.Count; i++)
            {
                var errorModel = new ErrorModel()
                {
                    Id = (excelWorksheet.Cells[i, 1] as Excel.Range).Value.ToString(),
                    Message = (excelWorksheet.Cells[i, 9] as Excel.Range).Value.ToString(),
                    Details = (excelWorksheet.Cells[i, 15] as Excel.Range).Value.ToString()
                };

                errorsList.Add(errorModel);
            }

            excelWorkbook.Close();
            excelApp.Quit();

            ;

        }
    }

    class ErrorModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

    }
}
