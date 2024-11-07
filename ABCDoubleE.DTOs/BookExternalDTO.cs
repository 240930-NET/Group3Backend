namespace ABCDoubleE.DTOs
{
    public class BookExternalDTO
    {
        public string title { get; set; }
        public string isbn { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public List<string> authors { get; set; } = new List<string>();
        public List<string> genres { get; set; } = new List<string>();
    }
}
