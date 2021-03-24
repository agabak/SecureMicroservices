using Movie.API.Data.Repositories.Repository;
using System;
using System.Threading.Tasks;

namespace Movie.API.Data.Repositories
{
    public interface IUnityOfWork: IDisposable
    {
        public IMovieRepository Movies { get; }
        Task SaveChanges();
    }
}
