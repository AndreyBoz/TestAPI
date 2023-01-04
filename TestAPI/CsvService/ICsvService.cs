using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
namespace TestAPI.CsvService
{
    public interface ICsvService
    {
        Task PostFile(IFormFile uploadedFile, HttpContext httpContext);
        Task<List<Result>> GetResult(Filter filter);
        Task<List<Value>> GetValues(string fileName);
    }
}
