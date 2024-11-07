using ABCDoubleE.Models;
using ABCDoubleE.Data;
using ABCDoubleE.DTOs;
using Microsoft.EntityFrameworkCore;


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

    public async Task<IEnumerable<Book>> GetBooksByBookshelfIdAsync(int bookshelfId)
    {
        var bookshelf = await _context.Bookshelves
            .Where(b => b.bookshelfId == bookshelfId)
            .Include(b => b.bookshelfBooks)
                .ThenInclude(bb => bb.book) 
            .ThenInclude(b => b.bookAuthors)
                .ThenInclude(ba => ba.author)
            .Include(b => b.bookshelfBooks)
                .ThenInclude(bb => bb.book)
                .ThenInclude(b => b.bookGenres) 
                .ThenInclude(bg => bg.genre)
            .FirstOrDefaultAsync();

        return bookshelf?.bookshelfBooks.Select(bb => bb.book) ?? Enumerable.Empty<Book>();
    }

public async Task<bool> AddBookToBookshelfAsync(int bookshelfId, BookExternalDTO book)
{
    // Check if the book already exists in the database (using ISBN or title as unique identifiers)
    var existingBook = await _context.Books
        .Include(b => b.bookAuthors).ThenInclude(ba => ba.author)
        .Include(b => b.bookGenres).ThenInclude(bg => bg.genre)
        .FirstOrDefaultAsync(b => b.isbn == book.isbn || b.title == book.title);

    if (existingBook == null)
    {
        // Step 1: Handle Authors
        var bookAuthors = new List<BookAuthor>();
        foreach (var authorName in book.authors)
        {
            var existingAuthor = await _context.Authors.FirstOrDefaultAsync(a => a.name == authorName);
            if (existingAuthor == null)
            {
                // Add new author to database if it doesn't exist
                existingAuthor = new Author { name = authorName };
                _context.Authors.Add(existingAuthor);
                await _context.SaveChangesAsync(); // Save new author
            }
            bookAuthors.Add(new BookAuthor { author = existingAuthor });
        }

        // Step 2: Handle Genres
        var bookGenres = new List<BookGenre>();
        foreach (var genreName in book.genres)
        {
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.name == genreName);
            if (existingGenre == null)
            {
                // Add new genre to database if it doesn't exist
                existingGenre = new Genre { name = genreName };
                _context.Genres.Add(existingGenre);
                await _context.SaveChangesAsync(); // Save new genre
            }
            bookGenres.Add(new BookGenre { genre = existingGenre });
        }

        // Step 3: Add the new book with its authors and genres
        var newBook = new Book
        {
            title = book.title,
            isbn = book.isbn,
            description = book.description,
            image = book.image,
            bookAuthors = bookAuthors,
            bookGenres = bookGenres
        };
        _context.Books.Add(newBook);
        await _context.SaveChangesAsync(); // Save the new book and its relationships

        existingBook = newBook; // Now we have an existingBook reference
    }

    // Step 4: Check if the book is already in the specified bookshelf to avoid duplicates
    var existingEntry = await _context.BookshelfBooks
        .FirstOrDefaultAsync(bb => bb.bookshelfId == bookshelfId && bb.bookId == existingBook.bookId);

    if (existingEntry != null)
    {
        return false; // Book is already in the bookshelf
    }

    // Step 5: Add the book to the bookshelf
    var bookshelfBook = new BookshelfBook
    {
        bookshelfId = bookshelfId,
        bookId = existingBook.bookId
    };

    _context.BookshelfBooks.Add(bookshelfBook);
    return await _context.SaveChangesAsync() > 0;
}



}