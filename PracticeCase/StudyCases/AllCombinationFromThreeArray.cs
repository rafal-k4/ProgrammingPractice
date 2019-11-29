using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PracticeCase.StudyCases
{
    class AllCombinationFromThreeArray : IStudy
    {
        public static List<Properties> HeadArr { get; set; }
        public static List<Properties> ChestArr { get; set; }
        public static List<Properties> LegsArr { get; set; }


        public AllCombinationFromThreeArray()
        {
            HeadArr = new List<Properties>
            {
                new Properties
                {
                    Name = "F",
                    Health = 270,
                    Speed = 8
                },
                new Properties
                {
                    Name = "I",
                    Health = 360,
                    Speed = 0
                },
                new Properties
                {
                    Name = "S",
                    Health = 490,
                    Speed = -8
                }
            };

            ChestArr = new List<Properties>
            {
                new Properties
                {
                    Name = "F",
                    Health = 270,
                    Speed = 8
                },
                new Properties
                {
                    Name = "I",
                    Health = 360,
                    Speed = 0
                },
                new Properties
                {
                    Name = "S",
                    Health = 490,
                    Speed = -8
                }
            };

            LegsArr = new List<Properties>
            {
                new Properties
                {
                    Name = "F",
                    Health = 135,
                    Speed = 4
                },
                new Properties
                {
                    Name = "I",
                    Health = 180,
                    Speed = 0
                },
                new Properties
                {
                    Name = "S",
                    Health = 245,
                    Speed = -4
                }
            };
        }

        public void Execute()
        {
            var shuffledList = ShuffleList(HeadArr, ChestArr, LegsArr);

            var grouppedList = shuffledList.GroupBy(x => x.Speed).Select(x => x.OrderByDescending(y => y.Health).FirstOrDefault());

            SaveDataToFile(grouppedList.ToList());
        }


        private static List<Properties> ShuffleList(List<Properties> headArr, List<Properties> chestArr, List<Properties> legsArr)
        {
            var result = new List<Properties>();
            foreach (var headItem in headArr)
            {
                foreach (var chestItem in chestArr)
                {
                    foreach (var legItem in legsArr)
                    {
                        result.Add(headItem + chestItem + legItem);
                    }
                }
            }
            return result;
        }


        private static void SaveDataToFile(List<Properties> shuffledList)
        {
            string fileName = "result.txt";
            string fullPath = Path.Combine(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName, fileName);

            try
            {
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }

                using (FileStream fs = File.Create(fullPath))
                {
                    foreach (var item in shuffledList)
                    {
                        fs.Write(new UTF8Encoding().GetBytes(item.ToString()));
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }




        public class Properties
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Speed { get; set; }

            public static Properties operator +(Properties item1, Properties item2)
            {
                return new Properties
                {
                    Name = $"{item1.Name} {item2.Name}",
                    Health = item1.Health + item2.Health,
                    Speed = item1.Speed + item2.Speed
                };
            }

            public override string ToString()
            {
                return $"{Name};{Health};{Speed}{Environment.NewLine}";
            }
        }
    }
}
