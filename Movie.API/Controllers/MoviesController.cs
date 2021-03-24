using Microsoft.AspNetCore.Mvc;
using Movie.API.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnityOfWork _unityOfWork;
        public MoviesController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entities.Movie>>> Get()
        {    
            var movies = await _unityOfWork.Movies.GetAll();
            return movies.ToList();
        }
    }
}
