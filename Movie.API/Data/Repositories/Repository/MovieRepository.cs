using Microsoft.EntityFrameworkCore;
using Movie.API.Data.DataContext;
using System.Threading.Tasks;

namespace Movie.API.Data.Repositories.Repository
{
    public class MovieRepository : Repository<Entities.Movie>, IMovieRepository
    {
        private readonly ApplicationContext _context;
        public MovieRepository(ApplicationContext context) :base(context)
        {
            _context = context;
        }

        public async Task<Entities.Movie> GetById(int id)
        {
            return await _context.Movies.SingleOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateMovie(Entities.Movie movie)
        {
            if (movie == null) return;
            _context.Attach(movie);
            _context.Entry(movie).State = EntityState.Modified;
        }
    }
}
