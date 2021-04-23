using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PracticeCase.StudyCases.UnitTests
{
    class ValidatingJson_By_ModelSchema : IStudy
    {
        /// <summary>
        /// This method takes schema based on the model with specified JsonProperty, compares it with example response in Json format
        /// and then validates it.
        /// As a summary possible exception is thrown with details about missing / additional properties
        /// </summary>
        public void Execute()
        {

            JSchemaGenerator generator = new JSchemaGenerator();
            generator.ContractResolver = new CamelCasePropertyNamesContractResolver();
            JObject response = JObject.Parse(_response);
            

            JSchema schema = generator.Generate(typeof(JsonModel));


            //schema.AllowAdditionalProperties = false;

            RejectAdditionalProperties(schema);
            if (!response.IsValid(schema, out IList<string> messages))
            {
                // In proper unit test project better throw more specified execption -> AssertionFailExeception
                throw new Exception(string.Join(Environment.NewLine, messages));
            }
            ;
        }

        public void RejectAdditionalProperties(JSchema schema)
        {
            schema.AllowAdditionalProperties = false;
            foreach (var s in schema.Properties.Values) RejectAdditionalProperties(s);
        }

        private static string _response =
            "{" +
            "   \"fullName\" : \"My Full Name\", " +
            "   \"name\" : \"My Name\", " +
            "   \"nestedProp\" : { " +
            "       \"age\" : 10, " +
            "       \"number\" : 20, " +
            "   }" +
            "}";

        private class JsonModel
        {
            public string Name { get; set; }
            [JsonProperty("nestedProp")]
            public JsonNestedModel Nested { get; set; }

            public class JsonNestedModel
            {
                public int Age { get; set; }
            }
        }

    }

    


}
