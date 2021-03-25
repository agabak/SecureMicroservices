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

        public async Task UpdateById(int id, Entities.Movie movie)
        {
            if (id <= 0 || movie is null) return;
            var editMovie =   await _context.Movies
                                    .SingleOrDefaultAsync(x => x.Id == id);

            if (editMovie is null) return;
            editMovie.Genre  = movie.Genre ?? editMovie.Genre;
            editMovie.Title  = movie.Title ?? editMovie.Title;
            editMovie.Rating = movie.Rating ?? editMovie.Rating;
            editMovie.ImageUrl = movie.ImageUrl ?? editMovie.ImageUrl;
            editMovie.Owner = movie.Owner ?? editMovie.Owner;
            editMovie.ReleaseDate = movie.ReleaseDate != editMovie.ReleaseDate
                                    ? movie.ReleaseDate : editMovie.ReleaseDate;
        }

        public void UpdateMovie(Entities.Movie movie)
        {
            if (movie == null) return;
            _context.Attach(movie);
            _context.Entry(movie).State = EntityState.Modified;
        }
    }
}
