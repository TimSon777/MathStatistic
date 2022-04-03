using Ardalis.GuardClauses;

namespace BL;

public static class Math
{
    //Построение интервального ряда (Минимум, Максимум, Количество интервалов)
    public static double Min(this IEnumerable<double> source)
    {
        var min = double.MaxValue;
        foreach (var n in Guard.Against.Null(source, nameof(source)))
        {
            if (min > n) min = n;
        }

        return min;
    }
    
    public static double  Max(this IEnumerable<double> source)
    {
        var max = double.MinValue;
        foreach (var n in Guard.Against.Null(source, nameof(source)))
        {
            if (max < n) max = n;
        }

        return max;
    }
}