namespace ABCDoubleE.Models;

public class PreferenceBook
{
    public int preferenceId { get; set; }
    public Preference preference { get; set; }
    
    public int bookId { get; set; }
    public Book book { get; set; }
}