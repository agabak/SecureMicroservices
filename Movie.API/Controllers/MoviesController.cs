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
            var movies = await _unityOfWork.Movies
                              .GetAllByFilter(null, x => x.OrderBy(x => x.Rating));
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.Movie>> Get(int id)
        {
            return await _unityOfWork.Movies
                          .FirstOrDefaultrFilter(x => x.Id == id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Entities.Movie movie)
        {
            _unityOfWork.Movies.UpdateMovie(movie);
            await _unityOfWork.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Entities.Movie movie)
        {
            await _unityOfWork.Movies.UpdateById(id, movie);
            await _unityOfWork.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Entities.Movie movie)
        {
            await _unityOfWork.Movies.Add(movie);
            await _unityOfWork.SaveChanges();
            return Ok();
        }

        [HttpPost("PostList")]
        public async Task<IActionResult> Post(
            IEnumerable<Entities.Movie> movies)
        {
            await _unityOfWork.Movies.AddRange(movies);
            await _unityOfWork.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Entities.Movie movie)
        {
             _unityOfWork.Movies.DeleteEntity(movie);
            await _unityOfWork.SaveChanges();
            return NotFound();
        }
    }
}
