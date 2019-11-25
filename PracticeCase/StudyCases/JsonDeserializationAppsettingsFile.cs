using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PracticeCase.StudyCases
{
    public class JsonDeserializationAppsettingsFile : IStudy
    {
        public void Execute()
        {
            var reader = new StreamReader(@"./BaseFixtures/appsettings.json");

            var text = reader.ReadToEnd();

            //Deserialization using anonymous objects
            var obj = new { Variables = new { var1 = "", var2 = "" } };
            var obj2 = new { VariablesTwo = new List<object>() { } };

            var FirstElement = JsonConvert.DeserializeObject(text, obj.GetType());
            var SecondElement = JsonConvert.DeserializeObject(text, obj2.GetType());

            //Deserialization using predefined class
            var FirstElementWithStongType = JsonConvert.DeserializeObject<JsonParent>(text);
            var SecondElementWithStongType = JsonConvert.DeserializeObject<JsonVar>(text);

            //Console.WriteLine($"")
        }
    }





    public class JsonChild
    {
        [JsonProperty(propertyName: "var1")]
        public string var1 { get; set; }
        [JsonProperty(propertyName: "var2")]
        public string var2 { get; set; }
    }

    public class JsonParent
    {
        [JsonProperty(propertyName: "Variables")]
        public JsonChild JsonChild { get; set; }
    }
   
    public class JsonVar
    {
        [JsonProperty(propertyName: "VariablesTwo")]
        public object[] values { get; set; }
    }
}
