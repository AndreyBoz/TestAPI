namespace TestAPI.Models
{
    public class FileItem
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime? TimeReceipt { get; set; } = DateTime.Now;
        public List<Value> Values { get; set; } = new List<Value>();    
        public Result Results { get; set; }
    }
}
