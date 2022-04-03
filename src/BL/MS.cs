using Ardalis.GuardClauses;
using BL.Exceptions;

namespace BL;

// ReSharper disable once InconsistentNaming
public static class MS
{
    public static double Average(this IEnumerable<double> source)
    {
        var sum = .0;
        var count = 0;
        
        foreach (var e in Guard.Against.Null(source, nameof(source)))
        {
            sum += e;
            count++;
        }

        return sum / Guard.Against.Zero(count, nameof(source));
    }

    public static double StandardDeviation(this double source) 
        => Math.Sqrt(Guard.Against.Negative(source));

    // public static double Median(this IEnumerable<Interval> source)
    // {
    //     var orderedIntervals = source.OrderBy(e => e.Count);
    //     foreach (var interval in intervals)
    //     {
    //         
    //     }
    // }
}