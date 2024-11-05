namespace ABCDoubleE.Models;
public class BookGenre
{
    public int bookId { get; set; }
    public Book book { get; set; }

    public int genreId { get; set; }
    public Genre genre { get; set; }
}