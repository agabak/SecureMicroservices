using System.Threading.Tasks;

namespace Movie.API.Data.Repositories.Repository
{
    public interface IMovieRepository : IRepository<Entities.Movie>
    {
        Task UpdateById(int id, Entities.Movie movie);
        void UpdateMovie(Entities.Movie movie);
    }
}
