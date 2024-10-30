using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;

public interface IBookRepo{

    // GET METHODS
    public List<Book> GetAllBooks();

    public Book GetBookByISBN(string isbn);

    //POST
    public void AddBook(Book book);

    //DELETE
    public void DeleteBook(Book book);


}