using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABCDoubleE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // Endpoint to add a new genre
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] Genre genre)
        {
            if (genre == null || string.IsNullOrWhiteSpace(genre.name))
            {
                return BadRequest("Genre name is required.");
            }

            var addedGenre = await _genreService.AddGenreAsync(genre);
            return CreatedAtAction(nameof(AddGenre), new { id = addedGenre.genreId }, addedGenre);
        }


        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(List<Genre>), 200)]
        public async Task<IActionResult> SearchGenres([FromQuery] string search = "")
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var genres = await _genreService.SearchGenresAsync(search);
            return Ok(genres);
        }
    }
}
