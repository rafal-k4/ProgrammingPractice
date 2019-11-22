using PracticeCase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace ConsoleNetCore30
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = typeof(IStudy).Assembly;

            //var projectDirectoryInfo = TryGetSolutionDirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            var types = assembly.GetTypes();

            var file = SearchForFile(types, projectDirectoryInfo);
        }

        private static FileInfo SearchForFile(Type[] types, DirectoryInfo projectDirectoryInfo)
        {

        }


    }
}
