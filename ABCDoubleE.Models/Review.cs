namespace ABCDoubleE.Models;
public class Review{
    public int reviewId {get; set; }
    public int rating {get; set; } = -1;
    public string review {get; set; } = "";

    //Foreign keys
    public int userId {get; set; }
    public User user {get; set; }

    public int bookId {get; set; }
    public Book book {get; set; }
}