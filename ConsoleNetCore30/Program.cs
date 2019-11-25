﻿using PracticeCase;
using PracticeCase.StudyCases;
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

            var fileSeekerHelper = new StudyFileSeeker(assembly.GetTypes().Where(x => x.IsClass && x.GetInterface(nameof(IStudy)) != null), AppDomain.CurrentDomain.BaseDirectory);

            var studyType = fileSeekerHelper.GetLatestStudyType();

            var instance = (IStudy)Activator.CreateInstance(studyType);

            while (true)
            {
                instance.Execute();

                Console.ReadKey();
            }
        }
    }
}
