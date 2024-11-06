using ABCDoubleE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCDoubleE.Services
{
    public interface IPreferenceService
    {
        Task<Preference> GetPreferenceByUserIdAsync(int userId);
        Task<Preference> CreatePreferenceAsync(int userId);
        Task UpdatePreferenceAsync(Preference preference);
        Task DeletePreferenceByUserIdAsync(int userId);
        Task<bool> AddGenreToPreferenceAsync(int userId, int genreId);
        Task<bool> AddAuthorToPreferenceAsync(int userId, int authorId);
        Task<bool> AddBookToPreferenceAsync(int userId, int bookId);

        Task<bool> RemoveGenreFromPreferenceAsync(int userId, int genreId);
        Task<bool> RemoveAuthorFromPreferenceAsync(int userId, int authorId);
        Task<bool> RemoveBookFromPreferenceAsync(int userId, int bookId);
    }
}
