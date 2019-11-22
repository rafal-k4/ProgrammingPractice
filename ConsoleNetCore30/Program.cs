using PracticeCase;
using ProgrammingPracticeHelper;
using System;
using System.Linq;



namespace ConsoleNetCore30
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = typeof(IStudy).Assembly;

            var fileSeekerHelper = new StudyFileSeeker(assembly.GetTypes().Where(x => x.IsClass), AppDomain.CurrentDomain.BaseDirectory);

            var studyType = fileSeekerHelper.GetLatestStudyType();

        }
    }
}
