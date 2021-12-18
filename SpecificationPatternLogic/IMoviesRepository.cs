using SpecificationPatternLogic.Entities;
using System.Collections.Generic;

namespace SpecificationPatternLogic
{
    public interface IMoviesRepository
    {
        IReadOnlyList<Movie> GetMovies(bool forKidsOnly, bool cdAvailable, int minRating);
        Movie GetById(long movieId);
        bool IsForChildOnly(Movie movie);
        bool IsCdAvailable(Movie movie);
    }
}