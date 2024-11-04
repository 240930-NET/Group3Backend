namespace ABCDoubleE.Models;
public class PreferenceAuthor
{
    public int preferenceId { get; set; }
    public Preference? preference { get; set; }
    
    public int authorId { get; set; }
    public Author? author { get; set; }
}