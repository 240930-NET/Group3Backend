namespace ABCDoubleE.Models;
public class Genre
{
    public int genreId { get; set; }
    public string name { get; set; } = "";
    public ICollection<BookGenre> bookGenres { get; set; } = new List<BookGenre>();
}