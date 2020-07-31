using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace PracticeCase.StudyCases
{
    class GettingValueFromPropertyUsingReflection : IStudy
    {
        public void Execute()
        {
            var obj = new A
            {
                Name = "aa",
                Array = new List<string> { "1", "2" },
                SomeClass = new B
                {
                    Count = 42,
                    C = new C
                    {
                        Title = "ASDASD",
                        Age = 101
                    }
                }
            };


            var props = obj.GetType().GetProperties();

            var nestedProp = props[2].PropertyType.GetProperties()[1].PropertyType.GetProperties()[0]; // its Title from class C -> "ASDASD"
            var nestedProp2 = props[2].PropertyType.GetProperties()[1].PropertyType.GetProperties()[1]; // its Age from class C -> 101


            var anonymoysOBJ = new {A = new {B = new {C = new {D = new {E = new {F = "Nested"}}}}}};

            var nested1 = GetPropertyValue(obj, "Name"); // "aa"
            var nested2 = GetPropertyValue(obj, "SomeClass.Count"); // 42
            var nested3 = GetPropertyValue(obj, "SomeClass.C.Title"); // "ASDASD"
            var nested4 = GetPropertyValue(anonymoysOBJ, "A.B.C.D.E.F"); // "Nested"


            ;
            
            var valueOfNestedProp = nestedProp.GetValue(obj.SomeClass.C); // "ASDASD"
            var valueOfNestedProp2 = nestedProp2.GetValue(obj.SomeClass.C); // 101

            var valueOfNestedProp3 = nestedProp.GetValue(obj, BindingFlags.Instance, null, null, CultureInfo.InvariantCulture); // System.Reflection.TargetException: 'Object does not match  
            var valueOfNestedProp4 = nestedProp2.GetValue(obj, BindingFlags.Instance, null, null, CultureInfo.InvariantCulture); // System.Reflection.TargetException: 'Object does not match 


            var valueOfNestedProp5 = nestedProp.GetValue(obj); // System.Reflection.TargetException: 'Object does not match 
            var valueOfNestedProp6 = nestedProp2.GetValue(obj); // System.Reflection.TargetException: 'Object does not match 

            
            
            ;
        }


        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null || propName == null)
            {
                return null;
            }

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }

    }



}
