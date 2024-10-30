namespace ABCDoubleE.Models;
public class Book{
    public int bookId {get; set;}
    public string isbn {get; set; } = "";
    public string description {get; set; } = "";
    public ICollection<BookshelfBook> bookshelfBooks { get; set; } = [];
    public ICollection<Review> reviewList {get;set;} = [];
}