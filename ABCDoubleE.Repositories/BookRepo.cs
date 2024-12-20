using ABCDoubleE.Models;
using ABCDoubleE.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Book>> SearchBooksAsync(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return await _context.Books
                .Include(b => b.bookAuthors)
                    .ThenInclude(ba => ba.author)
                .Include(b => b.bookGenres)
                    .ThenInclude(bg => bg.genre)
                .OrderBy(book => book.title)
                .Take(10)
                .ToListAsync();
        }

        return await _context.Books
            .Where(book => book.title.Contains(search))
            .Include(b => b.bookAuthors)
                .ThenInclude(ba => ba.author)
            .Include(b => b.bookGenres)
                .ThenInclude(bg => bg.genre)
            .ToListAsync();
    }



}