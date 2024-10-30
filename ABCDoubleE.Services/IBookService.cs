using ABCDoubleE.DTOs;
using ABCDoubleE.Models;

namespace ABCDoubleE.Services;

public interface IBookService{

    //GET METHODS
    public List<Book> GetAllBooks();

    public Book GetBookByISBN(string isbn);

    //PUT METHODS
    public string AddBook(BookDTO book);

    //DELETE METHODS
    public string DeleteBook(string isbn);
}

