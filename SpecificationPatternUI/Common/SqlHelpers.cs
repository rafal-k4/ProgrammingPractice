using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace SpecificationPatternUI.Common
{
    public static class SqlHelpers
    {
        public static void CreateDatabase(string connectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            string targetDatabase = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            if (DataBaseExists(builder.ConnectionString, targetDatabase))
                return;

            //var scriptFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatabaseWithDirector.sql");
            var projectDebugFilePath = AppDomain.CurrentDomain.BaseDirectory;
            var projectRootpath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(projectDebugFilePath).FullName).FullName).FullName).FullName;
            var sqlScriptFilePath = Path.Combine(projectRootpath, "DatabaseWithDirector.sql");
            var script = File.ReadAllText(sqlScriptFilePath);

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        new SqlCommand(commandString, connection).ExecuteNonQuery();
                    }
                }
            }
        }

        private static bool DataBaseExists(string connectionString, string databaseName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand($"SELECT db_id('{databaseName}')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }
    }
}
