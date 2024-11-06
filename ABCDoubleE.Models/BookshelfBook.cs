namespace ABCDoubleE.Models;
public class BookshelfBook{
    public int bookId {get; set; }
    public Book? book {get; set; }
    public int bookshelfId {get; set; }
    public Bookshelf? bookshelf {get; set; }
  
}