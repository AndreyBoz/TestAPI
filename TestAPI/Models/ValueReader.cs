using CsvHelper.Configuration.Attributes;

namespace TestAPI.Models
{
    public class ValueReader
    {
        [Index(0)]
        public string Time { get; set; }
        [Index(1)]
        public int Seconds { get; set; }
        [Index(2)]
        public double Number { get; set; }
    }
}
