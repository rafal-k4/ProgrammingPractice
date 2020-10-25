using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PracticeCase.StudyCases
{
    class ConsoleSetOut_ToFileStream : IStudy
    {
        public void Execute()
        {
            using var fs = new FileStream("output.txt", FileMode.Append);

            using var sw = new StreamWriter(fs);

            // These methods are writing directly to the file, anything that is going to be displayed normally on console (using Console.WriteLine())
            // right now is transferred to the file
            Console.SetOut(sw);
            



            using var fs2 = new FileStream("input.txt", FileMode.Open);
            using var sr = new StreamReader(fs2);

            //At this moment, instead of reading input from Console, the input is read from input.txt file,
            // so when every Console.ReadLine() is invoked, the line from file input.txt is read
            Console.SetIn(sr);
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                // This method instead of displaying line in console is writing new line to the file output.txt,
                // -----
                // so overall whole method is copying content from input.txt file to the output.txt
                // -----
                Console.WriteLine(line);
            }



            //sw.Flush();

            ;
        }
    }
}
