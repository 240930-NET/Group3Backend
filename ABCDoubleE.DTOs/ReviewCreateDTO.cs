namespace ABCDoubleE.DTOs;
public class ReviewCreateDTO
{
    public int rating { get; set; }
    public string reviewContent { get; set; }
    public int bookId { get; set; } 
    public int userId { get; set; } 
    public string reviewText {get; set; } = "";
}