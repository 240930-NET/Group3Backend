namespace ABCDoubleE.Models;

public class User
{
    public int userId {get; set; }
    public string userName {get; set; }= "";
    public string password {get; set; }= "";
    public string fullName {get; set; }= "";

    public Library library {get;set;}
    public int libraryId {get;set;}
    public ICollection<Review> reviewList {get;set;}
    public Preference preference {get;set;}
    public int preferenceId {get;set;}
}
