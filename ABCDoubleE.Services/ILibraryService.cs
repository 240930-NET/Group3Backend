using ABCDoubleE.DTOs;
using ABCDoubleE.Models;
public interface ILibraryService
{
    Task<LibraryDTO?> GetLibraryAsync(int libraryId);                    
    Task<IEnumerable<BookshelfDTO>> GetAllBookshelvesAsync(int libraryId); 
    Task<BookshelfDTO?> GetBookshelfByIdAsync(int libraryId, int bookshelfId); 
    Task<BookshelfDTO?> AddBookshelfAsync(int libraryId, BookshelfCreateDTO bookshelfCreateDto); 
    Task<bool> DeleteBookshelfAsync(int libraryId, int bookshelfId);  
    Task<bool> DeleteLibraryAsync(int libraryId);                   
}
