using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecificationPatternLogic.Entities
{
    [Table("Movie")]
    public class Movie
    {
        public long MovieId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime ReleaseDate { get; set; }
        public virtual MpaaRating MpaaRating { get; set; }
        public virtual string Genre { get; set; }
        public virtual double Rating { get; set; }
        public virtual Director Director { get; set; }
    }

    public enum MpaaRating
    {
        G = 1,
        PG = 2,
        PG13 = 3,
        R = 4
    }
}
