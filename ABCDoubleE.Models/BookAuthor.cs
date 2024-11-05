namespace ABCDoubleE.Models;
public class BookAuthor
{
    public int bookId { get; set; }
    public Book book { get; set; }

    public int authorId { get; set; }
    public Author author { get; set; }


    //public string role { get; set; } = "";

}
