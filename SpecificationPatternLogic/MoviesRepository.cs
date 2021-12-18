
using Microsoft.EntityFrameworkCore;
using SpecificationPatternLogic.Entities;
using System;
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

        public IReadOnlyList<Movie> GetMovies(bool forKidsOnly, bool cdAvailable, int minRating)
        {
            return dbContext.Movies
                .Where(x => x.MpaaRating <= MpaaRating.PG || !forKidsOnly)
                .Where(x => x.ReleaseDate <= DateTime.Now.AddMonths(-6) || !cdAvailable)
                .Where(x => x.Rating >= minRating)
                .Include(x => x.Director)
                .ToList();
        }
    }
}
