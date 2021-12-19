

using SpecificationPatternLogic.Entities;
using System;
using System.Linq.Expressions;

namespace SpecificationPatternLogic.Logic
{
    public class IsMovieForChildSpecification : SpecificationBase<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression() 
        {
            return movie => movie.MpaaRating <= MpaaRating.PG;
        } 
         
    }
}
