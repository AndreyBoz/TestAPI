namespace TestAPI.Models
{
    public class Filter
    {
        public string FileName { get; set; } = String.Empty;
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int? SecondsStart { get; set; }
        public int? SecondsEnd { get; set; }
        public int? NumberStart { get; set; }
        public int? NumberEnd { get; set; }
    }
}
