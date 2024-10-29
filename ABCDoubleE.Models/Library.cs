namespace ABCDoubleE.Models;

    public class Library
    {
        public int libraryId {get; set; }

        public ICollection<Bookshelf> bookshelfList {get; set; } = [];

        //Foreign key
        public int userId {get; set; }
        public User user {get; set; }
    }

