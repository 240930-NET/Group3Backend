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
    List<Bookshelf> result = _bookshelfrepo.getAllBookshelfRecords();
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
        Bookshelf bookshelf = _bookshelfrepo.getBookshelfByID(BookshelfId);
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
        if(bookshelf.List<Bookshelf> != null)
        {
            _bookshelfrepo.addBookshelf(bookshelf);
            return $"Bookshelf {bookshelf.List<Bookshelf>} added successfully!";
        }
        else 
        {
            throw new Exception("Invalid Bookshelf. Please check the list if empty or not!");
        }
    }

    public string DeleteBookshelf(Bookshelf BookshelfID)
    {
        Bookshelf searchedBookshelf = _bookshelfrepo.getBookshelfByID(BookshelfID);
        if(searchedBookshelf != null)
        {
            _bookshelfrepo.deleteBookshelf(searchedBookshelf);
            return $"Bookshelf with id {BookshelfID} deleted successfully!";
        }
        else
        {
            throw new Exception($"This bookshelf with id {BookshelfID} does not exist"); 
        }
    }

    public Bookshelf UpdateBookshelf(Bookshelf bookshelf)
    {
        Bookshelf searchedBookshelf = _bookshelfrepo.getBookshelfByID(bookshelf.BookshelfID);
        if(searchedBookshelf != null)
        {
            searchedBookshelf.List<Bookshelf>Bookshelf = bookshelf.List<Bookshelf>Bookshelf;
            _bookshelfrepo.updateBookshelf(searchedBookshelf);
            return searchedBookshelf
        }
        else
        {
            throw new Exception("Invalid Bookshelf. Please check if the bookshelf list is empty or not!");
        }
        else 
        {
            throw new Exception("Invalid Bookshelf!")
        }

    }

 












}