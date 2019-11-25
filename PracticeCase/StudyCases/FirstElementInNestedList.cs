using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeCase.StudyCases
{
    class FirstElementInNestedList : IStudy
    {
        public void Execute()
        {
            List<Tag> tagList = new List<Tag>() { new Tag() { name = "a" }, new Tag() { name = "b" }, new Tag() { name = "c" } }; 
            tagList[0].childTag.Add(new Tag() { name = "a1" });
            tagList[0].childTag.Add(new Tag() { name = "a2" });
            tagList[1].childTag.Add(new Tag() { name = "b3" });
            tagList[1].childTag.Add(new Tag() { name = "b4" });
            tagList[2].childTag.Add(new Tag() { name = "c5" });
            tagList[2].childTag.Add(new Tag() { name = "c6" });

            var tag = tagList.FirstOrDefault(x => x.childTag.Any(y => y.name == "c6"));
        }
    }




    public class Tag
    {
        public string name { get; set; }
        public List<Tag> childTag { get; set; } = new List<Tag>();
    }
}
