namespace Services;

public interface ICsvHelper
{
    Task<IEnumerable<List<double>>> ReadFileAsync(string pathToFile, string separator, params int[] columns);
}