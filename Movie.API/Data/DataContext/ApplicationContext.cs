using Microsoft.EntityFrameworkCore;

namespace Movie.API.Data.DataContext
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options) { }

        public DbSet<Entities.Movie> Movies { get; set; }
    }
}
