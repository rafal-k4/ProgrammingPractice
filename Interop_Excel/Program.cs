
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

            Excel.Workbook workbook = excelApp.Workbooks.Open(@"C:\Temp\ErrorsOutput.xlsx");

            Excel.Worksheet excelWorksheet = (Excel.Worksheet)workbook.Sheets[1];

            Excel.Range excelRange = excelWorksheet.UsedRange;

            var errorsList = new List<ErrorModel>();

            for (int i = 1; i <= excelRange.Rows.Count; i++)
            {
                var errorModel = new ErrorModel()
                {
                    Id = Guid.Parse(excelWorksheet.Cells[i, 1].ToString()),
                    Message = excelWorksheet.Cells[i, 9].ToString(),
                    Details = excelWorksheet.Cells[i, 15].ToString()
                };

                errorsList.Add(errorModel);
            }

            ;

        }
    }

    class ErrorModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

    }
}
