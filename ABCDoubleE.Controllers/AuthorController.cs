using ABCDoubleE.Models;
using ABCDoubleE.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABCDoubleE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // Endpoint to add a new author
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            if (author == null || string.IsNullOrWhiteSpace(author.name))
            {
                return BadRequest("Author name is required.");
            }

            var addedAuthor = await _authorService.AddAuthorAsync(author);
            return CreatedAtAction(nameof(AddAuthor), new { id = addedAuthor.authorId }, addedAuthor);
        }


        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(List<Author>), 200)]
        public async Task<IActionResult> SearchAuthors([FromQuery] string search = "")
        {
            var authors = await _authorService.SearchAuthorsAsync(search);
            return Ok(authors);
        }
    }
}
