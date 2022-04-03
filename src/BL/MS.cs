using Ardalis.GuardClauses;

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
        => System.Math.Sqrt(Guard.Against.Negative(source));

    public static double Median(this IEnumerable<Interval> source)
    {
        var orderedIntervals = source.OrderBy(e => e.Count).ToArray();
        var countElements = orderedIntervals.Sum(i => i.Count);
        var accumulatedCount = 0;
        var previousInterval = orderedIntervals[0];
        foreach (var interval in orderedIntervals)
        {
            accumulatedCount += interval.Count;
            
            if (System.Math.Abs(countElements - accumulatedCount / (double) 2) < 0.001)
            {
                return previousInterval.Left + (.5 * countElements);
            }
        }

        return default;
    }
}