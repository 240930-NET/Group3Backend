using ABCDoubleE.Repositories;
using ABCDoubleE.Models;
using ABCDoubleE.DTOs;

namespace ABCDoubleE.Services;

public class BookService : IBookService{

    private readonly IBookRepo _bookrepo;

    public BookService(IBookRepo bookrepo){
        _bookrepo = bookrepo;
    }

    //GET METHODS
    public List<Book> GetAllBooks(){
        
        List<Book> result = _bookrepo.GetAllBooks();

        if(result.Count ==0)
            return new List<Book>();

        else 
            return result;

    }

    public Book GetBookByISBN(string isbn){

        Book book = _bookrepo.GetBookByISBN(isbn);
        if(book==null)
            return new Book();
        else
            return book;
    }

    //PUT METHODS
    public string AddBook(BookDTO book){

        Book newbook = new(){
            isbn = book.isbn,
            description = book.description,
            bookshelfBooks = [],
            reviewList = []

        };

        if(book.isbn!=null){
            _bookrepo.AddBook(newbook);
            return $"Book added";
        }
        else{
            throw new Exception("Invalid ISBN");
        }

    }

    public string DeleteBook(string isbn){

        Book book = _bookrepo.GetBookByISBN(isbn);
        if(book != null){
            _bookrepo.DeleteBook(book!);
            return $"Book deleted succesfully with ISBN: {isbn}";
        }
        else
            throw new Exception($"This Book with ISBN: {isbn} doesn't exist");
    }

}

