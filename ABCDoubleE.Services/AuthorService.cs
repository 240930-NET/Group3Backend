using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using System.Threading.Tasks;

namespace ABCDoubleE.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            return await _authorRepository.AddAuthorAsync(author);
        }

        public async Task<List<Author>> SearchAuthorsAsync(string search)
        {
            return await _authorRepository.SearchAuthorsAsync(search);
        }
    }
}
