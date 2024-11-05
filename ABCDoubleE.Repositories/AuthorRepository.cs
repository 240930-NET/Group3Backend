using ABCDoubleE.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ABCDoubleE.Data;
using ABCDoubleE.Models;


namespace ABCDoubleE.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ABCDoubleEContext _context;

        public AuthorRepository(ABCDoubleEContext context)
        {
            _context = context;
        }

        public async Task<Author> AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<List<Author>> SearchAuthorsAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                // Return a default list of 10 authors if no search term is provided
                return await _context.Authors
                    .OrderBy(a => a.name)
                    .Take(10)
                    .ToListAsync();
            }
    
            return await _context.Authors
                .Where(a => a.name.Contains(search))
                .ToListAsync();
        }
    }
}
