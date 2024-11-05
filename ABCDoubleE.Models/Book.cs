namespace ABCDoubleE.Models;
public class Book{
    public int bookId {get; set;}
    public string isbn {get; set; } = "";
    public string title {get; set; } = "";
    public string description {get; set; } = "";
    public string image = "";
    public ICollection<BookshelfBook> bookshelfBooks { get; set; } = [];
    public ICollection<BookGenre> bookGenres { get; set; } = new List<BookGenre>();
    public ICollection<BookAuthor> bookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<Review> reviewList {get;set;} = [];
}