namespace ABCDoubleE.Models;
public class Genre
{
    public int genreId { get; set; }
    public string name { get; set; } = "";
    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
}