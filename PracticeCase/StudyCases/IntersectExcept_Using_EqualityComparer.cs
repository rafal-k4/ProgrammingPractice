using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace PracticeCase.StudyCases
{
    class IntersectExcept_Using_EqualityComparer : IStudy
    {
        public void Execute()
        {
            var firstList = new List<Person>
            {
                new Person("a", 1),
                new Person("b", 2),
                new Person("c", 3)
            };

            var secondList = new List<Person>
            {
                new Person("b", 2),
                new Person("c", 3),
                new Person("d", 4)
            };


            var resultOfIntersect = firstList.Intersect(
                secondList,
                EqualityComparer.Get<Person>((x, y) => x.Name == y.Name)).ToList(); // { b, 2 }, { c, 3 } 

            var resultOfExcept = firstList.Except(
                secondList,
                EqualityComparer.Get<Person>((x, y) => x.Name == y.Name)).ToList(); // { a, 1 }

            ;
        }
    }


    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;
        }


        public override string ToString()
        {
            return $"{Name} {Age}";
        }
    }
}
