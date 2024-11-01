using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;
public interface ILibraryRepository
{
    Task<Library?> GetLibraryByIdAsync(int libraryId);        
    Task<IEnumerable<Library>> GetAllLibrariesAsync();        
    Task AddLibraryAsync(Library library);                    
    Task UpdateLibraryAsync(Library library);                 
    Task DeleteLibraryAsync(int libraryId);                  

    Task<Bookshelf?> GetBookshelfByIdAsync(int libraryId, int bookshelfId); 
}
