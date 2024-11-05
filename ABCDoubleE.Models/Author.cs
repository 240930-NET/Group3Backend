namespace ABCDoubleE.Models;
public class Author
{
    public int authorId { get; set; }
    public string name { get; set; } = "";
    public ICollection<BookAuthor> bookAuthors { get; set; } = new List<BookAuthor>();
}