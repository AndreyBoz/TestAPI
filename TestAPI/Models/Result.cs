using System.Collections.Generic;

namespace TestAPI.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int FileItemId { get; set; } = 0;
        public FileItem File { get; set; } = null; // connect with fileitem table
        public int Count { get; set; } = 0;
        public int AllTime { get; set; } = 0;
        public int MiddleSeconds { get; set; } = 0;
        public DateTime? MinTime { get; set; } = null;
        public double MiddleNumber { get; set; } = 0;
        public double MedianNumber { get; set; } = 0;
        public double MaxNumber { get; set; } = 0;
        public double MinNumber { get; set; } = 0;
        public Result()
        {

        }
        public Result(List<Value> values) {
            this.FileItemId = values[0].FileItemId;
            this.Count = values.Count;

            MiddleNumber = values.Sum(x=>x.Number) / Count;
            MaxNumber = values.Max(x => x.Number);
            MinNumber = values.Min(x => x.Number);
            values.Sort(delegate (Value x, Value y) {
                return x.Number.CompareTo(y.Number);
            });
            MedianNumber = (values.Count % 2 != 0) ? values[values.Count / 2].Number : ((values[values.Count / 2].Number + values[(values.Count / 2) - 1].Number + 1) / 2);

            AllTime = values.Max(x => x.Seconds) - values.Min(x => x.Seconds);
            MiddleSeconds = values.Sum(x => x.Seconds) / Count;

            MinTime = values[0].File.TimeReceipt;
        }

    }
}
