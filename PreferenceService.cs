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
            var preference = new Preference { userId = userId };
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

        // Adding to preference collections
        public async Task AddGenreToPreferenceAsync(int userId, Genre genre)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference != null)
            {
                await _preferenceRepository.AddGenreToPreferenceAsync(preference.preferenceId, genre);
            }
        }

        public async Task AddAuthorToPreferenceAsync(int userId, Author author)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference != null)
            {
                await _preferenceRepository.AddAuthorToPreferenceAsync(preference.preferenceId, author);
            }
        }

        public async Task AddBookToPreferenceAsync(int userId, Book book)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference != null)
            {
                await _preferenceRepository.AddBookToPreferenceAsync(preference.preferenceId, book);
            }
        }

        // Removing from preference collections
        public async Task RemoveGenreFromPreferenceAsync(int userId, int genreId)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference != null)
            {
                await _preferenceRepository.RemoveGenreFromPreferenceAsync(preference.preferenceId, genreId);
            }
        }

        public async Task RemoveAuthorFromPreferenceAsync(int userId, int authorId)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference != null)
            {
                await _preferenceRepository.RemoveAuthorFromPreferenceAsync(preference.preferenceId, authorId);
            }
        }

        public async Task RemoveBookFromPreferenceAsync(int userId, int bookId)
        {
            var preference = await _preferenceRepository.GetPreferenceByUserIdAsync(userId);
            if (preference != null)
            {
                await _preferenceRepository.RemoveBookFromPreferenceAsync(preference.preferenceId, bookId);
            }
        }
    }
}
