using System;
using System.Threading.Tasks;


namespace PracticeCase.StudyCases
{
    class ConsoleKeyAvailable : IStudy
    {
        public void Execute()
        {
            //Console.ReadKey(true); // This reset KeyAvailable to false and doesn't display pressed key
            //Console.ReadKey(); // This reset KeyAvailable to false
            Console.ReadLine(); // This reset KeyAvailable to false

            Console.WriteLine("Press any key to break"); // any key press makes KeyAvailable to true!


            while (Console.KeyAvailable == false)
            {
                Console.WriteLine("Doing something!");
                Task.Delay(800).GetAwaiter().GetResult();
            }

        }
    }
}
