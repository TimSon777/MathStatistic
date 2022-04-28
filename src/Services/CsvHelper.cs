namespace Services;

public class CsvHelper : ICsvHelper
{
    public async Task<IEnumerable<List<double>>> ReadFileAsync(string pathToFile, string separator, params int[] columns)
    {
        var data = new List<double>[columns.Length];
        
        for (var j = 0; j < data.Length; j++)
        {
            data[j] = new List<double>();
        }
        
        foreach (var line in await File.ReadAllLinesAsync(pathToFile))
        {
            var values = line
                .Split(separator)
                .Select(double.Parse)
                .Where((_, i) => columns.Contains(i))
                .ToArray();

            for(var i = 0; i < columns.Length; i++)
            {
                data[i].Add(values[i]);
            }
        }

        return data;
    }
}