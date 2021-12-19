
using Microsoft.EntityFrameworkCore;
using SpecificationPatternLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public IReadOnlyList<Movie> GetMovies(Expression<Func<Movie, bool>> predicate, int minRating)
        {
            return dbContext.Movies
                .Where(predicate)
                .Where(x => x.Rating >= minRating)
                .Include(x => x.Director)
                .ToList();
        }
    }
}
