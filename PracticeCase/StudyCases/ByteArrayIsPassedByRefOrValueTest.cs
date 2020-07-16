using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class ByteArrayIsPassedByRefOrValueTest : IStudy
    {
        public void Execute()
        {
            var bytes = new byte[] {1, 2, 3};

            ModifySomeBytes(bytes);

            Console.WriteLine(string.Join(" | ", bytes)); // 2 | 4 | 6

            ModifySomeBytesByRef(ref bytes);

            Console.WriteLine(string.Join(" | ", bytes)); // 4 | 8 | 12
            ;
        }

        private void ModifySomeBytesByRef(ref byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += bytes[i];
            }
        }

        private void ModifySomeBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] += bytes[i];
            }
        }
    }
}
