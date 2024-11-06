namespace ABCDoubleE.Models;
public class Preference
{
    public int preferenceId { get; set; }
    public ICollection<PreferenceGenre> preferenceGenres { get; set; } = new List<PreferenceGenre>();
    public ICollection<PreferenceAuthor> preferenceAuthors { get; set; } = new List<PreferenceAuthor>();
    public ICollection<PreferenceBook> preferenceBooks { get; set; } = new List<PreferenceBook>();

    // Foreign key
    public int userId { get; set; }
    public User user { get; set; }
}
