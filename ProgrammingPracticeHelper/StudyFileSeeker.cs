using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProgrammingPracticeHelper
{
    public class StudyFileSeeker
    {
        private Type[] types { get; }
        private string projectBasePath { get; }

        public StudyFileSeeker(Type[] types, string projectBasePath)
        {
            this.types = types;
            this.projectBasePath = projectBasePath;
        }


        public Type GetLatestStudyType()
        {
            var solutionDirectoryInfo = TryGetSolutionDirectoryInfo();

            var LatestStudyType = Get

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
