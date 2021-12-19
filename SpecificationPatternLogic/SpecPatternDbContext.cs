using Microsoft.EntityFrameworkCore;
using SpecificationPatternLogic.Entities;
using System.Diagnostics.CodeAnalysis;

namespace SpecificationPatternLogic
{
    public class SpecPatternDbContext : DbContext
    {
        public SpecPatternDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
    }
}
