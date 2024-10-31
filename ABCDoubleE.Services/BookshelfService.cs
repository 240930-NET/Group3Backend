using ABCDoubleE.Repositories;
using ABCDoubleE.Models;
using ABCDoubleE.DTOs;

namespace ABCDoubleE.Services;

public class BookshelfService : IBookshelfService
{
    private readonly IBookshelfRepo _bookshelfrepo;

    public BookshelfService(IBookshelfRepo bookshelfrepo)
    {
        _bookshelfrepo = bookshelfrepo;
    }

    
   public List<Bookshelf> GetAllBookshelfRecords()
   {
    List<Bookshelf> result = _bookshelfrepo.GetAllBookshelfRecords();
    if(result.Count == 0)
    {
        return null;
    }
    else
    {
        return result;
    }
   }

    public Bookshelf GetBookshelfByID(int BookshelfId)
    {
        Bookshelf bookshelf = _bookshelfrepo.GetBookshelfByID(BookshelfId);
        if(bookshelf != null)
        {
            return bookshelf;
        }
        else
        {
            return null;
        }

    }

    public  string AddBookshelf(Bookshelf bookshelf)
    {
        if(bookshelf.name != null)
        {
            _bookshelfrepo.AddBookshelf(bookshelf);
            return $"Bookshelf {bookshelf.name} added successfully!";
        }
        else 
        {
            throw new Exception("Invalid Bookshelf. Please check the list if empty or not!");
        }
    }

    public string DeleteBookshelf(int BookshelfID)
    {
        Bookshelf searchedBookshelf = _bookshelfrepo.GetBookshelfByID(BookshelfID);
        if(searchedBookshelf != null)
        {
            _bookshelfrepo.DeleteBookshelf(searchedBookshelf);
            return $"Bookshelf with id {BookshelfID} deleted successfully!";
        }
        else
        {
            throw new Exception($"This bookshelf with id {BookshelfID} does not exist"); 
        }
    }

    public Bookshelf UpdateBookshelf(Bookshelf bookshelf)
    {
        Bookshelf searchedBookshelf = _bookshelfrepo.GetBookshelfByID(bookshelf.bookshelfId);
        if(searchedBookshelf != null)
        {
            searchedBookshelf.name = bookshelf.name;
            _bookshelfrepo.UpdateBookshelf(searchedBookshelf);
            return searchedBookshelf;
        }
        else
        {
            throw new Exception("Invalid Bookshelf. Please check if the bookshelf list is empty or not!");
        }
        

    }

}