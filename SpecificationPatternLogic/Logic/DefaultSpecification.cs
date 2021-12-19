using SpecificationPatternLogic.Entities;
using System;
using System.Linq.Expressions;

namespace SpecificationPatternLogic.Logic
{
    public class DefaultSpecification : SpecificationBase<Movie>
    {
        public override Expression<Func<Movie, bool>> SpecExpression { get; } = movie => true;
    }
}
