namespace ABCDoubleE.DTOs;

public class PreferenceCreateDTO
{
    public ICollection<string> favGenres { get; set; } = new List<string>();
    public ICollection<int> favBookIds { get; set; } = new List<int>();
    public ICollection<string> favAuthors { get; set; } = new List<string>();
}