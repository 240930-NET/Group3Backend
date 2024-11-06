using ABCDoubleE.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ABCDoubleE.Data;
using ABCDoubleE.Models;


namespace ABCDoubleE.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ABCDoubleEContext _context;

        public GenreRepository(ABCDoubleEContext context)
        {
            _context = context;
        }

        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<List<Genre>> SearchGenresAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                // Return a default list of 10 genres if no search term is provided
                return await _context.Genres
                    .OrderBy(g => g.name)
                    .Take(10)
                    .ToListAsync();
            }
    
            return await _context.Genres
                .Where(g => g.name.Contains(search))
                .ToListAsync();
        }
    }
}
