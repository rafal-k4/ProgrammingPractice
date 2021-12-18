
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

        public bool IsForChildOnly(Movie movie)
        {
            return movie.MpaaRating <= MpaaRating.PG;
        }

        public bool IsCdAvailable(Movie movie)
        {
            return movie.ReleaseDate <= DateTime.Now.AddMonths(-6);
        }

        public Movie GetById(long movieId)
        {
            return dbContext.Movies.First(x => x.MovieId == movieId);
        }

        public IReadOnlyList<Movie> GetMovies(bool forKidsOnly, bool cdAvailable, int minRating)
        {
            return dbContext.Movies
                // solution .ToList() ??
                .Where(x => IsForChildOnly(x) || !forKidsOnly)
                .Where(x => IsCdAvailable(x) || !cdAvailable)
                .Where(x => x.Rating >= minRating)
                .Include(x => x.Director)
                .ToList();
        }
    }
}
