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
        
        // Additional methods associated collections May implement later for efficient add and delete
        //Task AddGenreToPreferenceAsync(int preferenceId, Genre genre);
        //Task AddAuthorToPreferenceAsync(int preferenceId, Author author);
        //Task AddBookToPreferenceAsync(int preferenceId, Book book);
        //Task RemoveGenreFromPreferenceAsync(int preferenceId, int genreId);
        //Task RemoveAuthorFromPreferenceAsync(int preferenceId, int authorId);
        //Task RemoveBookFromPreferenceAsync(int preferenceId, int bookId);
    }
}
