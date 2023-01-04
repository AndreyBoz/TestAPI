namespace TestAPI.Models
{
    public class Value
    {
        public int Id { get; set; }
        public int FileItemId { get; set; }
        public FileItem? File { get; set; } // connect with fileitem table
        public DateTime? Time { get; set; }
        public int Seconds { get; set; } = 0;
        public double Number { get; set; } = 0;
    }
}
