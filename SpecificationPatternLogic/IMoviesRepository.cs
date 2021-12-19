using SpecificationPatternLogic.Entities;
using SpecificationPatternLogic.Logic;
using System.Collections.Generic;

namespace SpecificationPatternLogic
{
    public interface IMoviesRepository
    {
        IReadOnlyList<Movie> GetMovies(SpecificationBase<Movie> specification, int minRating);
        Movie GetById(long movieId);
    }
}