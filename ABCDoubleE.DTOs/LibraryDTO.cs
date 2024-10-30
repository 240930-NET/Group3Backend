namespace ABCDoubleE.DTOs;

public class LibraryDTO
{
    public int libraryId { get; set; }

    public List<BookshelfDTO> bookshelfList { get; set; } = new List<BookshelfDTO>(); 
}
