

using System.ComponentModel.DataAnnotations.Schema;

namespace SpecificationPatternLogic.Entities
{
    [Table("Director")]
    public class Director
    {
        public int DirectorId { get; set; }
        public virtual string Name { get; set; }
    }
}
