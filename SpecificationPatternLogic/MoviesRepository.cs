using Microsoft.EntityFrameworkCore;
using SpecificationPatternLogic.Entities;
using SpecificationPatternLogic.Logic;
using System.Collections.Generic;
using System.Linq;

namespace SpecificationPatternLogic
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly SpecPatternDbContext dbContext;
        public MoviesRepository(SpecPatternDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Movie GetById(long movieId)
        {
            return dbContext.Movies.First(x => x.MovieId == movieId);
        }

        public IReadOnlyList<Movie> GetMovies(Specification<Movie> specification, int minRating)
        {
            return dbContext.Movies
                .Where(specification.ToExpression())
                .Where(x => x.Rating >= minRating)
                .Include(x => x.Director)
                .ToList();
        }
    }
}
