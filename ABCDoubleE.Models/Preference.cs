using System.ComponentModel.DataAnnotations;

namespace ABCDoubleE.Models;
public class Preference{

    [Key]
    public int preferenceId {get; set; }
    public ICollection<string> favGenres {get; set; }= [];
    public ICollection<Book> favBooks {get; set; }= [];
    public ICollection<string> favAuthors {get; set; }= [];

    //Foreign key
    public int userId{get; set; }
    public User user{get; set; }
}