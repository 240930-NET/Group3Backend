namespace ABCDoubleE.Models;
public class PreferenceGenre
{
    public int preferenceId { get; set; }
    public Preference preference { get; set; }
    
    public int genreId { get; set; }
    public Genre genre { get; set; }
}