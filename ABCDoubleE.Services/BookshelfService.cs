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

    public  string AddBookshelf(newBookshelfDTO newbookshelfDTO)
    {
        Bookshelf bookshelf = new(){
            name = newbookshelfDTO.name

        };
        if(newbookshelfDTO.name != null)
        {
            _bookshelfrepo.AddBookshelf(bookshelf);
            return $"Bookshelf {bookshelf.name} added successfully!";
        }
        else 
        {
            throw new Exception("Invalid Bookshelf. Please check the list if empty or not!");
        }
    }

    public string DeleteBookshelf(int BookshelfId)
    {
        Bookshelf SearchedBookshelf = _bookshelfrepo.GetBookshelfByID(BookshelfId);
        if(SearchedBookshelf != null)
        {
            _bookshelfrepo.DeleteBookshelf(SearchedBookshelf);
            return $"Bookshelf with id {BookshelfId} deleted successfully!";
        }
        else
        {
            throw new Exception($"This bookshelf with id {BookshelfId} does not exist"); 
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