using ABCDoubleE.Data;
using ABCDoubleE.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCDoubleE.Repositories
{
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly ABCDoubleEContext _context;

        public PreferenceRepository(ABCDoubleEContext context)
        {
            _context = context;
        }

        public async Task<Preference> CreatePreferenceAsync(Preference preference)
        {
            _context.Preferences.Add(preference);
            await _context.SaveChangesAsync();
            return preference;
        }

        public async Task<Preference> GetPreferenceByUserIdAsync(int userId)
        {
            return await _context.Preferences
                .Include(p => p.preferenceGenres)
                    .ThenInclude(pg => pg.genre)
                .Include(p => p.preferenceAuthors)
                    .ThenInclude(pa => pa.author)
                .Include(p => p.preferenceBooks)
                    .ThenInclude(pb => pb.book)
                .FirstOrDefaultAsync(p => p.userId == userId);
        }

        public async Task UpdatePreferenceAsync(Preference preference)
        {
            _context.Preferences.Update(preference);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePreferenceByUserIdAsync(int userId)
        {
            var preference = await GetPreferenceByUserIdAsync(userId);
            if (preference != null)
            {
                _context.Preferences.Remove(preference);
                await _context.SaveChangesAsync();
            }
        }

        // Methods for adding to collections
        public async Task AddGenreToPreferenceAsync(int preferenceId, Genre genre)
        {
            var preferenceGenre = new PreferenceGenre
            {
                preferenceId = preferenceId,
                genreId = genre.genreId
            };
            _context.Set<PreferenceGenre>().Add(preferenceGenre);
            await _context.SaveChangesAsync();
        }

        public async Task AddAuthorToPreferenceAsync(int preferenceId, Author author)
        {
            var preferenceAuthor = new PreferenceAuthor
            {
                preferenceId = preferenceId,
                authorId = author.authorId
            };
            _context.Set<PreferenceAuthor>().Add(preferenceAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task AddBookToPreferenceAsync(int preferenceId, Book book)
        {
            var preferenceBook = new PreferenceBook
            {
                preferenceId = preferenceId,
                bookId = book.bookId
            };
            _context.Set<PreferenceBook>().Add(preferenceBook);
            await _context.SaveChangesAsync();
        }

    }
}
