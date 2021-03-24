namespace Movie.API.Data.Repositories.Repository
{
    public interface IMovieRepository : IRepository<Entities.Movie>
    {
        void UpdateMovie(Entities.Movie movie);
    }
}
