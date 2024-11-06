using ABCDoubleE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCDoubleE.Repositories
{
    public interface IPreferenceRepository
    {
        Task<Preference> CreatePreferenceAsync(Preference preference);
        Task<Preference> GetPreferenceByUserIdAsync(int userId);
        Task UpdatePreferenceAsync(Preference preference);
        Task DeletePreferenceByUserIdAsync(int userId);
        Task<Preference> GetPreferenceWithGenresByUserIdAsync(int preferenceId);
        Task<Preference> GetPreferenceWithAuthorsByUserIdAsync(int preferenceId);
        Task<Preference> GetPreferenceWithBooksByUserIdAsync(int preferenceId);

        Task RemoveGenreFromPreferenceAsync(PreferenceGenre genre);
        Task RemoveAuthorFromPreferenceAsync(PreferenceAuthor author);
        Task RemoveBookFromPreferenceAsync(PreferenceBook book);
        
        // Additional methods associated collections May implement later for efficient add and delete
        Task<bool> AddGenreToPreferenceAsync(Preference preference, int genreId);
        Task<bool> AddAuthorToPreferenceAsync(Preference preference, int authorId);
        Task<bool> AddBookToPreferenceAsync(Preference preference, int bookId);
    }

}
