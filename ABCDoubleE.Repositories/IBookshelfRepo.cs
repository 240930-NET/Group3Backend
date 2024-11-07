using ABCDoubleE.Models;
using ABCDoubleE.DTOs;
namespace ABCDoubleE.Repositories;


public interface IBookshelfRepo
{
    public List<Bookshelf> GetAllBookshelfRecords();

    public Bookshelf GetBookshelfByID(int BookshelfId);

    public  void AddBookshelf(Bookshelf bookshelf);

    public void DeleteBookshelf(Bookshelf bookshelf);

    public void UpdateBookshelf(Bookshelf bookshelf);

    Task<IEnumerable<Book>> GetBooksByBookshelfIdAsync(int bookshelfId);

    Task<bool> AddBookToBookshelfAsync(int bookshelfId, BookExternalDTO bookExternalDTO);
}