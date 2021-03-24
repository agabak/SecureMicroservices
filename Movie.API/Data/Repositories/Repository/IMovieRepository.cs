using System.Threading.Tasks;

namespace Movie.API.Data.Repositories.Repository
{
    public interface IMovieRepository : IRepository<Entities.Movie>
    {
        Task<Entities.Movie> GetById(int id);

        void UpdateMovie(Entities.Movie movie);
    }
}
