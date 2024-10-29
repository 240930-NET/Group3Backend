public class Book{
    public int bookId;
    public string isbn {get; set; } = "";
    public string description {get; set; } = "";
    public ICollection<BookshelfBook> bookshelfBooks { get; set; };
}