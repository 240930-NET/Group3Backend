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
        //add in later for efficient update
        //Task AddGenreToPreferenceAsync(int userId, Genre genre);
        //Task AddAuthorToPreferenceAsync(int userId, Author author);
        //Task AddBookToPreferenceAsync(int userId, Book book);

        Task<bool> RemoveGenreFromPreferenceAsync(int userId, int genreId);
        Task<bool> RemoveAuthorFromPreferenceAsync(int userId, int authorId);
        Task<bool> RemoveBookFromPreferenceAsync(int userId, int bookId);
    }
}
