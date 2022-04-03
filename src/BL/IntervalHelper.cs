using System.Collections;

namespace BL;

public static class IntervalHelper
{
    //Построение интервального ряда (Минимум, Максимум, Количество интервалов)
    public static int NumberIntervals(this IEnumerable<double> source, bool isRoundUp = false) 
    {
        var n = source.Count();
        var m = 1 + 3.322 * System.Math.Log10(n);
        
        return isRoundUp ? (int) System.Math.Ceiling(m) : (int) System.Math.Floor(m);
    }

    public static double IntervalWidth(this IEnumerable<double> source)
    {
        var max = source.Max();
        var min = source.Min();
        var m = source.NumberIntervals();
        var k = (max - min) / m;
        return k;
    }
    
    public static int FindCount(this IEnumerable<double> source, double start, double end) => 
        source.Count(n => n >= start && n < end);

    public static IEnumerable<Interval> GetIntervals(this IEnumerable<double> source)
    {
        var width = source.IntervalWidth();
        var delta = width / 10;
        var start = source.Min() - delta;
        var list = new List<Interval>();
        for (var i = 0; i < source.NumberIntervals(); i++)
        {
            var end = start + width;
            list.Add(new Interval(start, end, source.FindCount(start, end)));
            start = end;
        }

        return list;
    }
}