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

        public async Task<Preference> GetPreferenceWithGenresByUserIdAsync(int preferenceId)
        {
            return await _context.Preferences
                .Include(p => p.preferenceGenres)
                .FirstOrDefaultAsync(p => p.preferenceId == preferenceId);
        }

        public async Task<Preference> GetPreferenceWithAuthorsByUserIdAsync(int preferenceId)
        {
            return await _context.Preferences
                .Include(p => p.preferenceAuthors)
                .FirstOrDefaultAsync(p => p.preferenceId == preferenceId);
        }

        public async Task<Preference> GetPreferenceWithBooksByUserIdAsync(int preferenceId)
        {
            return await _context.Preferences
                .Include(p => p.preferenceBooks)
                .FirstOrDefaultAsync(p => p.preferenceId == preferenceId);
        }

        public async Task RemoveGenreFromPreferenceAsync(PreferenceGenre genre)
        {
            _context.PreferenceGenres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAuthorFromPreferenceAsync(PreferenceAuthor author)
        {
            _context.PreferenceAuthors.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookFromPreferenceAsync(PreferenceBook book)
        {
            _context.PreferenceBooks.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddGenreToPreferenceAsync(Preference preference, int genreId)
        {
            preference.preferenceGenres.Add(new PreferenceGenre { genreId = genreId, preferenceId = preference.preferenceId });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddAuthorToPreferenceAsync(Preference preference, int authorId)
        {
            preference.preferenceAuthors.Add(new PreferenceAuthor { authorId = authorId, preferenceId = preference.preferenceId });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddBookToPreferenceAsync(Preference preference, int bookId)
        {
            preference.preferenceBooks.Add(new PreferenceBook { bookId = bookId, preferenceId = preference.preferenceId });
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
