using ABCDoubleE.Models;
using ABCDoubleE.Data;

namespace ABCDoubleE.Repositories;

public class BookshelfRepo : IBookshelfRepo
{
    private readonly ABCDoubleEContext _context;

    public BookshelfRepo(ABCDoubleEContext context){
        _context = context;
    }
    public List<Bookshelf> GetAllBookshelfRecords()
    {
        return _context.Bookshelves.ToList();
    }

    public Bookshelf GetBookshelfByID(int Id)
    {
        return _context.Bookshelves.FirstOrDefault(b => b.bookshelfId == Id);
    }

    public  void AddBookshelf(Bookshelf bookshelf)
    {
         _context.Bookshelves.Add(bookshelf);
         _context.SaveChanges();

    }

    public void DeleteBookshelf(Bookshelf Bookshelf)
    {
        _context.Bookshelves.Remove(Bookshelf);
        _context.SaveChanges();
    }

    public void UpdateBookshelf(Bookshelf bookshelf)
    {
        _context.Bookshelves.Update(bookshelf);
        _context.SaveChanges();
    }















}