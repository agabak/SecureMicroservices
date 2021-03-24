using Microsoft.EntityFrameworkCore;
using Movie.API.Data.DataContext;
using Movie.API.Data.Repositories.Repository;
using System.Threading.Tasks;

namespace Movie.API.Data.Repositories
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly ApplicationContext _context;

        public UnityOfWork(ApplicationContext context)
        {
            _context = context;
            Movies = new MovieRepository(context);
        }

        public IMovieRepository Movies { get; }

        public async Task SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw new DbUpdateConcurrencyException(ex.Message);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
