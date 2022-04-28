using System.Collections.ObjectModel;
using BL;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Desktop.Controllers;

public class DataController : Controller
{
    private ICsvHelper CsvHelper { get; set; }

    public DataController(ICsvHelper csvHelper)
    {
        CsvHelper = csvHelper;
    }

    [HttpGet]
    public IActionResult DataEntry()
    {
        return View();
    }

    [HttpPost]
    public async Task<ViewResult> GetData(string pathToFile, string separator, string columns, double probability = .95)
    {
        var columnsArray = columns.Split(',')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToArray();
        
       var data = await CsvHelper.ReadFileAsync(pathToFile, separator, columnsArray);
       var result = data.Select(x => Statistic.WithAllParams(x, probability));
       return View(result);
    }
}