using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;

public interface IBookshelfService
{
   public List<BookShelf> GetAllBookshelfRecords();

    public BookShelf GetBookshelfByID(int BookshelfId);

    public  void AddBookshelf(Bookshelf bookshelf);

    public void DeleteBookshelf(Bookshelf bookshelf);

    public void UpdateBookshelf(Bookshelf bookshelf);
 
}
