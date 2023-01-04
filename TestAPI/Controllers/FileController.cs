using System.Diagnostics;
using CsvHelper;
using TestAPI.CsvService;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TestAPI.Models;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;

namespace TestAPI.Controllers
{
    public class FileController : Controller
    {
        private readonly ICsvService _csvService;
        public FileController(ICsvService csvService) { 
            _csvService = csvService;
        }
        
        [HttpPost("/api/PostFile")]
        async public Task<ActionResult> PostFile(IFormFile uploadedFile) {
            await _csvService.PostFile(uploadedFile, HttpContext);
            return Ok(); 
        }
        [HttpPost("/api/GetResult")]
        async public Task<List<Result>> GetResult(Filter filter)
        {
            return await _csvService.GetResult(filter);
        }
        [HttpPost("/api/GetValues")]
        async public Task<List<Value>> GetValues(string fileName)
        {
            return await _csvService.GetValues(fileName);
        }
    }
}
