using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace PracticeCase.StudyCases
{
    class GettingPropertiesAndSubPropertiesOfAClassByAttributeRecursiveMethod : IStudy
    {
        public void Execute()
        {
            var obj = new A
            {
                Name = "aa",
                Array = new List<string> {"1", "2"},
                SomeClass = new B
                {
                    Count = 42,
                    C = new C
                    {
                        Age = 101,
                        Title = "ASDASD"
                    }
                }
            };

            var properties = obj.GetType().GetProperties();


            var prop1 = properties[0].GetType().GetProperties(); //propertyInfo[15]
            var prop2 = properties[1].GetType().GetProperties(); //propertyInfo[15]
            var prop3 = properties[2].GetType().GetProperties(); //propertyInfo[15]

            var isClass = properties[2].PropertyType.IsClass;
            var propType = properties[0].PropertyType.GetProperties(); //propertyInfo[2] -> is string so Chars and Length
            var propType2 = properties[1].PropertyType.GetProperties(); //propertyInfo[0] 
            var propType3 = properties[2].PropertyType.GetProperties(); //propertyInfo[1] -> only Count from B class
            var propType4 = properties[3].PropertyType.GetProperties(); //propertyInfo[16] -> a lot properties from DateTime

            ;


            var propWithAttributes = properties[3].GetCustomAttributes(typeof(RequiredAttribute), true);

            var propIsDefined = properties[3].IsDefined(typeof(RequiredAttribute), true); // true
            var propIsNotDefined = properties[2].IsDefined(typeof(RequiredAttribute), true); // false





            //==============Retrieving Properties By Attribute=========================

            var result = GetPropertiesByAttribute<RequiredAttribute>(obj);

            ;

        }

        public Dictionary<string, string> GetPropertiesByAttribute<TAttribute>(object model)
        {
            var result = new Dictionary<string, string>();

            if (model == null)
            {
                return result;
            }

            foreach (var propertyInfo in model.GetType().GetProperties())
            {

                if (IsPropertyClassAndNotStringAndNotDateTime(propertyInfo.PropertyType))
                {
                    var nestedProperties = GetPropertiesByAttribute<TAttribute>(propertyInfo.GetValue(model));

                    foreach (var nestedProperty in nestedProperties)
                    {
                        result.TryAdd(nestedProperty.Key, nestedProperty.Value);
                    }
                }

                if (propertyInfo.IsDefined(typeof(TAttribute), true))
                {
                    result.TryAdd(propertyInfo.Name, propertyInfo.GetValue(model)?.ToString());
                }
                
            }

            return result;
        }

        private bool IsPropertyClassAndNotStringAndNotDateTime(Type propertyType)
        {
            return propertyType.IsClass && propertyType != typeof(string) && propertyType != typeof(DateTime);
        }
    }



    public class A
    {
        public string Name { get; set; }

        public IEnumerable<string> Array { get; set; }

        public B SomeClass { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }

    public class B
    {
        [Required]
        public int Count { get; set; }

        public C C { get; set; }
    }

    public class C
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
