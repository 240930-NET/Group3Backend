using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;

public interface IBookshelfService
{
   public List<Bookshelf> GetAllBookshelfRecords();

    public Bookshelf GetBookshelfByID(int BookshelfId);

    
    public  string AddBookshelf(newBookshelfDTO newbookshelfDTO);

    public string DeleteBookshelf(int BookshelfId);

    public Bookshelf UpdateBookshelf(Bookshelf bookshelf);
 
}
