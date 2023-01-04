using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TestAPI.Models;

namespace TestAPI.CsvService
{
    public class CsvService : ICsvService
    {
        ApplicationContext db;
        public CsvService(ApplicationContext context)
        {
            db = context;
        }

        public async Task<List<Result>> GetResult(Filter filter)
        {
            List<Result> Results = db.Results.ToList();
            if (filter.FileName != String.Empty)
                Results.Where(f => f.File.FileName == filter.FileName);
            if (filter.SecondsEnd != null && filter.SecondsStart != null)
                Results.Where(f => f.MiddleSeconds <= filter.SecondsEnd && f.MiddleSeconds >= filter.SecondsStart);
            if (filter.NumberEnd != null && filter.NumberStart != null)
                Results.Where(f => f.MiddleNumber <= filter.NumberEnd && f.MiddleNumber >= filter.NumberStart);
            if (filter.TimeEnd != null && filter.TimeStart != null)
                Results.Where(f => f.MinTime <= filter.TimeEnd && f.MinTime >= filter.TimeStart);
            return Results;
        }

        public async Task<List<Value>> GetValues(string fileName)
        {
            var sqlRequest = $"USE bozhov; SELECT * FROM[bozhov].[dbo].[Values] WHERE FileItemId = (Select Id From[bozhov].[dbo].[FileItems] WHERE FileName = '{fileName}')";
            List <Value> values = db.Values.FromSqlRaw(sqlRequest).ToList();
            return values;
        }

        public async Task PostFile(IFormFile uploadedFile, HttpContext httpContext)
        {
            if (db.FileItems.FirstOrDefault(f => f.FileName == uploadedFile.FileName) != null)
            {
                db.FileItems.Remove(db.FileItems.FirstOrDefault(f => f.FileName == uploadedFile.FileName));
            }
            var values = new List<Value>();
            int countLines = 0;
            using (var reader = new StreamReader(uploadedFile.OpenReadStream()))
            {
                // create time object
                FileItem item = new FileItem() { FileName = uploadedFile.FileName, TimeReceipt = DateTime.Now };
                db.Add(item);
                db.SaveChanges();

                var _item = db.FileItems.FirstOrDefault(f => f.FileName == item.FileName); // take new id from bd
                var config = new CsvConfiguration(new CultureInfo("de-DE"))
                {
                    HasHeaderRecord = false,
                    Delimiter = ";",
                };
                using var csv = new CsvReader(reader, config); // using CSVReader to read the csvfile 
                await foreach (var val in csv.GetRecordsAsync<ValueReader>(httpContext.RequestAborted))
                {
                    // validation test 
                    /*if (!TryValidateModel(val))
                        return ValidationProblem();*/

                    countLines++;
                    if (countLines >= 10000)
                        throw new NotImplementedException();

                    _item.Values.Add(new Value
                    {
                        FileItemId = _item.Id,
                        Number = val.Number,
                        Time = DateTime.ParseExact(val.Time, "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture),
                        Seconds = val.Seconds,
                    });

                }
                db.SaveChanges();

                // table result
                Result result = new Result(_item.Values);
                db.Add(result);
                db.SaveChanges();
            }
        }
    }
}
