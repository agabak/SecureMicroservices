using Microsoft.EntityFrameworkCore;
using Movie.API.Data.DataContext;

namespace Movie.API.Data.Repositories.Repository
{
    public class MovieRepository : Repository<Entities.Movie>, IMovieRepository
    {
        private readonly ApplicationContext _context;
        public MovieRepository(ApplicationContext context) :base(context)
        {
            _context = context;
        }
        public void UpdateMovie(Entities.Movie movie)
        {
            if (movie == null) return;
            _context.Attach(movie);
            _context.Entry(movie).State = EntityState.Modified;
        }
    }
}
