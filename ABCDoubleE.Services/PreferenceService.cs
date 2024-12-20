using ABCDoubleE.Models;
using ABCDoubleE.Repositories;
using System.Threading.Tasks;

namespace ABCDoubleE.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;

        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }

        public async Task<Preference> GetPreferenceByUserIdAsync(int userId)
        {
            return await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
        }

        public async Task<Preference> CreatePreferenceAsync(int userId)
        {
            var preference = new Preference 
                { 
                    userId = userId,
                };
            return await _preferenceRepository.CreatePreferenceAsync(preference);
        }

        public async Task UpdatePreferenceAsync(Preference preference)
        {
            await _preferenceRepository.UpdatePreferenceAsync(preference);
        }

        public async Task DeletePreferenceByUserIdAsync(int userId)
        {
            await _preferenceRepository.DeletePreferenceByUserIdAsync(userId);
        }

        public async Task<bool> RemoveGenreFromPreferenceAsync(int preferenceId, int genreId)
        {
            var preference = await _preferenceRepository.GetPreferenceWithGenresByUserIdAsync(preferenceId);
            var genre = preference?.preferenceGenres.FirstOrDefault(g => g.genreId == genreId);
            if (genre == null) return false;

            await _preferenceRepository.RemoveGenreFromPreferenceAsync(genre);
            return true;
        }

        public async Task<bool> RemoveAuthorFromPreferenceAsync(int preferenceId, int authorId)
        {
            var preference = await _preferenceRepository.GetPreferenceWithAuthorsByUserIdAsync(preferenceId);
            var author = preference?.preferenceAuthors.FirstOrDefault(a => a.authorId == authorId);
            if (author == null) return false;

            await _preferenceRepository.RemoveAuthorFromPreferenceAsync(author);
            return true;
        }

        public async Task<bool> RemoveBookFromPreferenceAsync(int preferenceId, int bookId)
        {
            var preference = await _preferenceRepository.GetPreferenceWithBooksByUserIdAsync(preferenceId);
            var book = preference?.preferenceBooks.FirstOrDefault(b => b.bookId == bookId);
            if (book == null) return false;

            await _preferenceRepository.RemoveBookFromPreferenceAsync(book);
            return true;
        }
        public async Task<bool> AddGenreToPreferenceAsync(int userId, int genreId)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference == null || preference.preferenceGenres.Any(g => g.genreId == genreId))
                return false;

            var success = await _preferenceRepository.AddGenreToPreferenceAsync(preference, genreId);
            return success;
        }

        public async Task<bool> AddAuthorToPreferenceAsync(int userId, int authorId)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference == null || preference.preferenceAuthors.Any(a => a.authorId == authorId))
                return false;

            var success = await _preferenceRepository.AddAuthorToPreferenceAsync(preference, authorId);
            return success;
        }

        public async Task<bool> AddBookToPreferenceAsync(int userId, int bookId)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference == null || preference.preferenceBooks.Any(b => b.bookId == bookId))
                return false;

            var success = await _preferenceRepository.AddBookToPreferenceAsync(preference, bookId);
            return success;
        }
    }
}
