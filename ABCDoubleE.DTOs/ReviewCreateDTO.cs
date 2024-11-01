namespace ABCDoubleE.DTOs;

public class ReviewCreateDTO{
    public int rating {get; set; } = -1;
    public string review {get; set; } = "";

    public int userId {get; set; }

    public int bookId {get; set; }
}