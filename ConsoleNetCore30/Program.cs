using PracticeCase;
using ProgrammingPracticeHelper;
using System;
using System.Linq;
using System.Reflection;

namespace ConsoleNetCore30
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = typeof(IStudy).Assembly;

            var path = Assembly.GetEntryAssembly().Location;

            var fileSeekerHelper = new StudyFileSeeker(assembly.GetTypes().Where(x => x.IsClass && x is IStudy), AppDomain.CurrentDomain.BaseDirectory);

            var studyType = fileSeekerHelper.GetLatestStudyType();

        }
    }
}
