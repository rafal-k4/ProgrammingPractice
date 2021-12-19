using SpecificationPatternLogic.Entities;
using System;
using System.Linq.Expressions;

namespace SpecificationPatternLogic.Logic
{
    public class IsCdAvailableSpecification : SpecificationBase<Movie>
    {
        public const int MONTHS_TO_CD_RELEASE = -6;
        public override Expression<Func<Movie, bool>> SpecExpression { get; } = 
            movie => movie.ReleaseDate <= DateTime.Now.AddMonths(MONTHS_TO_CD_RELEASE);
    }
}
