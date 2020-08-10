using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCase.StudyCases
{
    class DeadlockTests : IStudy
    {
        public void Execute()
        {
            // to experiment with ASP.NET Core deadlock and WPF .Net Core Deadlock go to:
            // 1. DeadlockTestsWPF.MainWindow.xaml.cs  ----> deadlock acquired
            // 2. TypeScriptPractice.Controllers.HomeController ----> deadlock not possible because of .net core
            //                                                        and it's lack of Synchronization Context https://blog.stephencleary.com/2017/03/aspnetcore-synchronization-context.html
            //
            //
            // According to this article: https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
            // Deadlock should happen in UI application like WPF, WinForms as well as in ASP.NET
            // because of thread blocking context (in case of UI it is UI Thread Context and in ASP.NET it is ASP.NET request context)
        }


    }
}
