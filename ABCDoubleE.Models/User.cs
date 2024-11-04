namespace ABCDoubleE.Models;

public class User
{
    public int userId {get; set; }
    public string userName {get; set; }= "";
    public string passwordHash {get; set; }= "";
    public string passwordSalt {get;set;} = "";
    public string fullName {get; set; }= "";

    public required Library library {get;set;}
    public int libraryId {get;set;}
    public ICollection<Review> reviewList {get;set;}
    public Preference preference {get;set;}
    public int preferenceId {get;set;}
}
