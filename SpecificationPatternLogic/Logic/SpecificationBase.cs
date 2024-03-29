﻿using System;
using System.Linq;
using System.Linq.Expressions;


namespace SpecificationPatternLogic.Logic
{
    public abstract class SpecificationBase<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public SpecificationBase<T> And(SpecificationBase<T> secondSpec)
        {
            return new AndSpecification<T>(this, secondSpec);
        }

        public SpecificationBase<T> Or(SpecificationBase<T> secondSpec)
        {
            return new OrSpecification<T>(this, secondSpec);
        }

        public SpecificationBase<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }

    // NOTE THE ACCESS MODIFIER
    internal class AndSpecification<T> : SpecificationBase<T>
    {
        private readonly SpecificationBase<T> firstSpec;
        private readonly SpecificationBase<T> secondSpec;

        public AndSpecification(SpecificationBase<T> firstSpec, SpecificationBase<T> secondSpec)
        {
            this.firstSpec = firstSpec;
            this.secondSpec = secondSpec;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var firstExpression = firstSpec.ToExpression();
            var secondExpression = secondSpec.ToExpression();

            InvocationExpression invokeExpression = Expression.Invoke(secondExpression, firstExpression.Parameters);

            BinaryExpression combinedExpression = Expression.AndAlso(firstExpression.Body, invokeExpression);

            return Expression.Lambda<Func<T, bool>>(combinedExpression, firstExpression.Parameters);   
        }
    }

    internal class OrSpecification<T> : SpecificationBase<T>
    {
        private readonly SpecificationBase<T> firstSpec;
        private readonly SpecificationBase<T> secondSpec;

        public OrSpecification(SpecificationBase<T> firstSpec, SpecificationBase<T> secondSpec)
        {
            this.firstSpec = firstSpec;
            this.secondSpec = secondSpec;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var firstExpression = firstSpec.ToExpression();
            var secondExpression = secondSpec.ToExpression();

            InvocationExpression invokeExpression = Expression.Invoke(secondExpression, firstExpression.Parameters);

            BinaryExpression combinedExpression = Expression.OrElse(firstExpression.Body, invokeExpression);

            return Expression.Lambda<Func<T, bool>>(combinedExpression, firstExpression.Parameters);
        }
    }

    internal class NotSpecification<T> : SpecificationBase<T>
    {
        private readonly SpecificationBase<T> firstSpec;

        public NotSpecification(SpecificationBase<T> firstSpec)
        {
            this.firstSpec = firstSpec;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {            
            var firstExpression = firstSpec.ToExpression();

            UnaryExpression combinedLogicOrBinaryExpression = Expression.Not(firstExpression.Body);

            return Expression.Lambda<Func<T, bool>>(combinedLogicOrBinaryExpression, firstExpression.Parameters.Single());   
        }
    }
}
