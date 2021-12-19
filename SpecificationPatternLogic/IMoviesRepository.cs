using SpecificationPatternLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpecificationPatternLogic
{
    public interface IMoviesRepository
    {
        IReadOnlyList<Movie> GetMovies(Expression<Func<Movie, bool>> predicate, int minRating);
        Movie GetById(long movieId);
    }
}