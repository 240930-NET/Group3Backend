namespace ABCDoubleE.Models;
public class Bookshelf{
    public int bookshelfId {get; set; }
    public ICollection<Book> listOfBooks {get; set; } = [];

    //Foreign key
    public int libraryId {get; set; }
    public Library library {get; set; }
    public ICollection<BookshelfBook> bookshelfBooks { get; set; }

}