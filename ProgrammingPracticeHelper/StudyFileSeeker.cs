using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProgrammingPracticeHelper
{
    public class StudyFileSeeker
    {
        private IEnumerable<Type> types { get; }
        private string projectBasePath { get; }

        public StudyFileSeeker(IEnumerable<Type> types, string projectBasePath)
        {
            if(types.Any() == false)
            {
                throw new Exception("No types provided");
            }
            this.types = types;
            this.projectBasePath = projectBasePath;
        }


        public Type GetLatestStudyType()
        {
            var solutionDirectoryInfo = TryGetSolutionDirectoryInfo();

            return GetLatestWrittenStudyType(solutionDirectoryInfo);
        }



        private Type GetLatestWrittenStudyType(DirectoryInfo solutionDirectoryInfo)
        {
            var allFiles = new List<FileSeekerModel>();

            foreach (var item in types)
            {
                var resultFile = SearchForFile(solutionDirectoryInfo, item.Name);
                if (resultFile.Any())
                {
                    allFiles.Add(new FileSeekerModel { FileType = item, FileInfo = resultFile.First() });
                }
            }

            if (allFiles.Any())
            {
                return allFiles.OrderByDescending(x => x.FileInfo.LastWriteTime).First().FileType;
            }

            throw new FileNotFoundException($"File Not Found");
        }

        private FileInfo[] SearchForFile(DirectoryInfo solutionDirectoryInfo, string name)
        {
            return solutionDirectoryInfo.GetFiles($"{name}*", SearchOption.AllDirectories);
        }

        private DirectoryInfo TryGetSolutionDirectoryInfo()
        {
            var directory = new DirectoryInfo(projectBasePath ?? Directory.GetCurrentDirectory());

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory;
        }
    }
}
