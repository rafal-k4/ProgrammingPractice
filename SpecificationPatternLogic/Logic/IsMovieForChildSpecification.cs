

using SpecificationPatternLogic.Entities;
using System;
using System.Linq.Expressions;

namespace SpecificationPatternLogic.Logic
{
    public class IsMovieForChildSpecification : SpecificationBase<Movie>
    {
        public override Expression<Func<Movie, bool>> Expression { get; set; } = 
            movie => movie.MpaaRating <= MpaaRating.PG;
    }
}
