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
            throw new Exception("No books in the database.");

        else 
            return result;

    }

    public Book GetBookByISBN(string isbn){

        Book book = _bookrepo.GetBookByISBN(isbn);
        if(book==null)
            throw new Exception($"Can't find book by ISBN: {isbn}!");
        else
            return book;
    }

    //PUT METHODS
    public string AddBook(BookDTO book){

        Book newbook = new(){
            isbn = book.isbn,
            title = book.title,
            description = book.description,
            bookshelfBooks = [],
            reviewList = []

        };

        Book searchedBook = _bookrepo.GetBookByISBN(newbook.isbn);

        if(book.isbn!=""&&searchedBook==null){
            _bookrepo.AddBook(newbook);
            return $"Book added";
        }
        if(searchedBook!=null&&searchedBook.isbn==newbook.isbn){
            throw new Exception("Duplicate ISBN");
        }
        else{
            throw new Exception("Invalid empty ISBN");
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

    public async Task<List<Book>> SearchBooksAsync(string search)
    {
        return await _bookrepo.SearchBooksAsync(search);
    }

}

