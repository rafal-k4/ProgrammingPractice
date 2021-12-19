//using System;
//using System.Linq;
//using System.Linq.Expressions;


//namespace SpecificationPatternLogic.Logic
//{
//    public abstract class SpecificationBase<T>
//    {
//        public abstract Expression<Func<T, bool>> ToExpression();

//        public bool IsSatisfiedBy(T entity)
//        {
//            var predicate = ToExpression().Compile();
//            return predicate(entity);
//        }

//        public SpecificationBase<T> And(SpecificationBase<T> secondSpec)
//        {
//            return new AndSpecification<T>(this, secondSpec);
//        }

//        public SpecificationBase<T> Or(SpecificationBase<T> secondSpec)
//        {
//            return new OrSpecification<T>(this, secondSpec);
//        }

//        public SpecificationBase<T> Not()
//        {
//            return new NotSpecification<T>(this);
//        }
//    }

//    // NOTE THE ACCESS MODIFIER
//    internal class AndSpecification<T> : SpecificationBase<T>
//    {
//        private readonly SpecificationBase<T> firstSpec;
//        private readonly SpecificationBase<T> secondSpec;

//        public AndSpecification(SpecificationBase<T> firstSpec, SpecificationBase<T> secondSpec)
//        {
//            this.firstSpec = firstSpec;
//            this.secondSpec = secondSpec;
//        }

//        public override Expression<Func<T, bool>> ToExpression()
//        {
//            Expression<Func<Entities.Movie, bool>> firstExpression = m => m.Name == "The Secret Life of Pets";
//            //var secondExpression = secondSpec.ToExpression();
//            var aaa = firstSpec.ToExpression();
//            Expression<Func<Entities.Movie, bool>> secondExpression = m => m.ReleaseDate <= DateTime.Now.AddMonths(-6);

//            ParameterExpression expressionParameter = Expression.Parameter(typeof(Entities.Movie), "m");
//            //var secondExpressionInvoked = Expression.Invoke(secondExpression, expressionParameter);

//            BinaryExpression combinedLogicAndBinaryExpression = Expression.AndAlso(firstExpression.Body, secondExpression.Body);


//            Expression<Func<Entities.Movie, bool>> expr = movie => (int)movie.MpaaRating <= 2 && movie.ReleaseDate <= DateTime.Now.AddMonths(-6);
//            var res = Expression.Lambda<Func<T, bool>>(expr.Body, expr.Parameters);
//            var res2 = Expression.Lambda<Func<T, bool>>(combinedLogicAndBinaryExpression, firstExpression.Parameters[0]);
//            var paramres = firstExpression.Parameters[0];
//            //return Expression.Lambda<Func<T, bool>>(expr.Body, expr.Parameters);
//            return Expression.Lambda<Func<T, bool>>(combinedLogicAndBinaryExpression, expressionParameter);

//        }
//    }

//    internal class OrSpecification<T> : SpecificationBase<T>
//    {
//        private readonly SpecificationBase<T> firstSpec;
//        private readonly SpecificationBase<T> secondSpec;

//        public OrSpecification(SpecificationBase<T> firstSpec, SpecificationBase<T> secondSpec)
//        {
//            this.firstSpec = firstSpec;
//            this.secondSpec = secondSpec;
//        }

//        public override Expression<Func<T, bool>> ToExpression()
//        {
//            var firstExpression = firstSpec.ToExpression();
//            var secondExpression = secondSpec.ToExpression();

//            BinaryExpression combinedLogicOrBinaryExpression = Expression.OrElse(firstExpression.Body, secondExpression.Body);

//            return Expression.Lambda<Func<T, bool>>(combinedLogicOrBinaryExpression, firstExpression.Parameters.Single());

//        }
//    }

//    internal class NotSpecification<T> : SpecificationBase<T>
//    {
//        private readonly SpecificationBase<T> firstSpec;

//        public NotSpecification(SpecificationBase<T> firstSpec)
//        {
//            this.firstSpec = firstSpec;
//        }

//        public override Expression<Func<T, bool>> ToExpression()
//        {
//            var firstExpression = firstSpec.ToExpression();

//            UnaryExpression combinedLogicOrBinaryExpression = Expression.Not(firstExpression.Body);

//            return Expression.Lambda<Func<T, bool>>(combinedLogicOrBinaryExpression, firstExpression.Parameters.Single());

//        }
//    }
//}


using System;
using System.Linq;
using System.Linq.Expressions;
using SpecificationPatternLogic.Entities;

namespace SpecificationPatternLogic.Logic
{
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
    }

    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new IdentitySpecification<T>();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            if (this == All)
                return specification;
            if (specification == All)
                return this;

            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (this == All || specification == All)
                return All;

            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public static Specification<T> operator &(Specification<T> lhs, Specification<T> rhs) => lhs.And(rhs);
        public static Specification<T> operator |(Specification<T> lhs, Specification<T> rhs) => lhs.Or(rhs);
        public static Specification<T> operator !(Specification<T> spec) => spec.Not();

    }

    internal sealed class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, Boolean>>)Expression.Lambda(Expression.AndAlso(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }

    internal sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _right = right;
            _left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);

            return (Expression<Func<T, Boolean>>)Expression.Lambda(Expression.OrElse(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }

    internal sealed class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _specification;

        public NotSpecification(Specification<T> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> expression = _specification.ToExpression();
            UnaryExpression notExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(notExpression, expression.Parameters.Single());
        }
    }

    public sealed class MovieForKidsSpecification : Specification<Movie>
    {
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating <= MpaaRating.PG;
        }
    }

    public sealed class AvailableOnCDSpecification : Specification<Movie>
    {
        private const int MonthsBeforeDVDIsOut = 6;

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.ReleaseDate <= DateTime.Now.AddMonths(-MonthsBeforeDVDIsOut);
        }
    }

    public sealed class MovieDirectedBySpecification : Specification<Movie>
    {
        private readonly string _director;

        public MovieDirectedBySpecification(string director)
        {
            _director = director;
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Director.Name == _director;
        }
    }
}