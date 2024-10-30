using ABCDoubleE.Models;
using ABCDoubleE.Data;

namespace ABCDoubleE.Repositories;

public class BookshelfRepo : IBookshelfRepo
{
    private readonly ABCDoubleEContext _context;

    public BookShelfRepo(ABCDoubleEContext context){
        _context = context;
    }
    public List<BookShelf> getAllBookshelfRecords()
    {
        return _context.Bookshelves.ToList();
    }

    public BookShelf getBookshelfByID(int BookshelfId)
    {
        return _context.Bookshelves.FirstOrDefault(b => b.BookshelfId == BookshelfId);
    }

    public  void addBookshelf(Bookshelf bookshelf)
    {
         _context.Bookshelves.add(bookshelf);
         _context.SaveChanges();

    }

    public void deleteBookshelf(Bookshelf bookshelf)
    {
        _context.Bookshelves.Remove(bookshelf);
        _context.SaveChanges();
    }

    public void updateBookshelf(Bookshelf bookshelf)
    {
        _context.Bookshelves.Update(bookshelf);
        _context.SaveChanges();
    }















}