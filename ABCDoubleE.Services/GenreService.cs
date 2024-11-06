using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using System.Threading.Tasks;

namespace ABCDoubleE.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            return await _genreRepository.AddGenreAsync(genre);
        }

        public async Task<List<Genre>> SearchGenresAsync(string search)
        {
            return await _genreRepository.SearchGenresAsync(search);
        }
    }
}
