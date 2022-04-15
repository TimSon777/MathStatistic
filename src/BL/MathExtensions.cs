using BL.Exceptions;

namespace BL;

public static class MathExtensions
{
    public static bool Between<T>(this T source, T left, T right)
        where T : IComparable => 
        source.CompareTo(left) >= 0 && source.CompareTo(right) <= 0;

    public static double Sqr(this double source) => source * source;

    public static double Cub(this double source) => source.Sqr() * source;

    public static double Pow(this double source, int power)
    {
        var result = source;
        for (var i = 0; i < power - 1; i++)
        {
            result *= source;
        }

        return result;
    }

    public static T Min<T>(this IEnumerable<T> source)
        where T : IComparable =>
        MaxOrMin(source, false);

    public static T Max<T>(this IEnumerable<T> source)
        where T : IComparable =>
        MaxOrMin(source, true);

    private static T MaxOrMin<T>(IEnumerable<T> source, bool isMax)
        where T : IComparable
    {
        var isFirstElement = true;
        var t = default(T);

        foreach (var e in source)
        {
            if (isFirstElement)
            {
                isFirstElement = false;
                t = e;
                continue;
            }

            switch (e.CompareTo(t))
            {
                case > 0 when isMax:
                case < 0 when !isMax:
                    t = e;
                    break;
            }
        }

        if (isFirstElement)
        {
            throw new NoElementsException(nameof(source));
        }

        return t!;
    }
}