using System;
using System.Linq.Expressions;


namespace SpecificationPatternLogic.Logic
{
    public abstract class SpecificationBase<T>
    {
        public abstract Expression<Func<T, bool>> Expression { get; set; }

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = Expression.Compile();
            return predicate(entity);
        }
    }
}
