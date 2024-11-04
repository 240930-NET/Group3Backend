using ABCDoubleE.Models;

namespace ABCDoubleE.Repositories;

public interface IBookshelfRepo
{
    public List<Bookshelf> GetAllBookshelfRecords();

    public Bookshelf GetBookshelfByID(int BookshelfId);

    public  void AddBookshelf(Bookshelf bookshelf);

    public void DeleteBookshelf(Bookshelf bookshelf);

    public void UpdateBookshelf(Bookshelf bookshelf);





}