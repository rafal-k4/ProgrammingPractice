
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace Interop_Excel
{
    class Program
    {

        private static Regex mailMatch = new Regex(@"([a-zA-Z0-9 +._,-]+@[a-zA-Z0-9 ._,->]*[\.,][a-zA-Z0-9 _-]+)", RegexOptions.Multiline);

        private static Regex defaultAccountMailMatch = new Regex(@"(?=.*[0-9])(?=.*[A-z])[A-Za-z0-9]{8}@(aboutstyle.co.uk|aboustyle.co.uk)", RegexOptions.IgnoreCase);

        static void Main(string[] args)
        {

            List<ErrorModel> errorsList = GetDataFromExcel(@"C:\Temp\Errors_LOCAL.xlsx", out Excel.Application excelApp, out Excel.Workbook excelWorkbook);

            List<ErrorModel> dataWithEmails = FilterDataWithMails(errorsList);

            CreateSqlScript(dataWithEmails);


            excelWorkbook.Close();
            excelApp.Quit();

            ;

        }

        private static void CreateSqlScript(List<ErrorModel> dataWithEmails)
        {
            File.WriteAllText(
                @"C:\Temp\Result-select.sql", 
                $"SELECT * {Environment.NewLine}" +
                $"FROM [SaleErrorLogging].[dbo].[aspnet_WebEvent_Events] {Environment.NewLine}" +
                $"WHERE EventId in ('{string.Join($"', {Environment.NewLine}'", dataWithEmails.Select(x => x.Id))}')");

            File.WriteAllText(
                @"C:\Temp\Result-delete.sql",
                $"DELETE FROM [SaleErrorLogging].[dbo].[aspnet_WebEvent_Events] {Environment.NewLine}" +
                $"WHERE EventId in ('{string.Join($"', {Environment.NewLine}'", dataWithEmails.Select(x => x.Id))}')");

        }

        private static string GetDeleteScriptContent(List<ErrorModel> dataWithEmails)
        {
            StringBuilder deleteScript = new StringBuilder();
            deleteScript.Append("DELETE FROM [SaleErrorLogging].[dbo].[aspnet_WebEvent_Events] WHERE [EventId] in (");
            foreach (var errorModel in dataWithEmails)
            {
                deleteScript.AppendLine(
                    $" = '{errorModel.Id}'");
                deleteScript.AppendLine();
            }

            return deleteScript.ToString();
        }

        private static List<ErrorModel> FilterDataWithMails(List<ErrorModel> errorsList)
        {
            var dataWithEmails = errorsList.Where(x =>
            {
                var matchDetailsWithEmails = mailMatch.Matches(x.Details);

                var areMailsInDetailsOnlyTestOrTemplateEmails = matchDetailsWithEmails.All(y =>
                    y.Value.Contains("user@domain")
                    || defaultAccountMailMatch.IsMatch(y.Value));

                var matchMessagesWithEMails = mailMatch.Matches(x.Message);

                var areMailsInMessageOnlyTestOrTemplateEmails = matchMessagesWithEMails.All(y =>
                    y.Value.Contains("user@domain")
                    || defaultAccountMailMatch.IsMatch(y.Value));

                return !areMailsInDetailsOnlyTestOrTemplateEmails || !areMailsInMessageOnlyTestOrTemplateEmails;
            }).ToList();

            return dataWithEmails;
        }

        private static List<ErrorModel> GetDataFromExcel(string pathToExcelFile, out Excel.Application excelApp, out Excel.Workbook excelWorkbook)
        {
            excelApp = new Excel.Application
            {
                Visible = false
            };

            excelWorkbook = excelApp.Workbooks.Open(pathToExcelFile);

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

            return errorsList;
        }
    }

    class ErrorModel
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

    }
}
