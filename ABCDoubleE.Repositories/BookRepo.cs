using ABCDoubleE.Models;
using ABCDoubleE.Data;

namespace ABCDoubleE.Repositories;

public class BookRepo : IBookRepo{
 
    private readonly ABCDoubleEContext _context;

    public BookRepo(ABCDoubleEContext context){
        _context = context;
    }

    //GET METHODS
    public List<Book> GetAllBooks(){
        return _context.Books.ToList();
    }

    public Book GetBookByISBN(string isbn){
        return _context.Books.FirstOrDefault(b => b.isbn==isbn)!;
    }

    //PUT METHOD
    public void AddBook(Book book){
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    //DELETE METHOD
    public void DeleteBook(Book book){
        _context.Books.Remove(book);
        _context.SaveChanges();
    }

}