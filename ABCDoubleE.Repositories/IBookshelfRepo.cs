using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;

public interface IBookshelfRepo
{
    public List<BookShelf> getAllBookshelfRecords();

    public BookShelf getBookshelfByID(int BookshelfId);

    public  void addBookshelf(Bookshelf bookshelf);

    public void deleteBookshelf(Bookshelf bookshelf);

    public void updateBookshelf(Bookshelf bookshelf);





}