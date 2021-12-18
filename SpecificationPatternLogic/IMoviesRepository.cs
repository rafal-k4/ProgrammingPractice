using SpecificationPatternLogic.Entities;
using System.Collections.Generic;

namespace SpecificationPatternLogic
{
    public interface IMoviesRepository
    {
        IReadOnlyList<Movie> GetMovies(bool forKidsOnly, bool cdAvailable, int minRating);
    }
}