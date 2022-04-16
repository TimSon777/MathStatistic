namespace BL;

public record Interval(double Left, double Right, int Frequency)
{
    public double Middle => (Left + Right) / 2;
    public double Length => Right - Left;

    public static Interval Default => new(0, 0, 0);

    public static Interval operator +(Interval a, Interval b)
    {
        var left = Math.Min(a.Left, b.Left);
        var right = Math.Max(a.Right, b.Right);
        var frequency = a.Frequency + b.Frequency;
        return new Interval(left, right, frequency);
    }

    public static Interval Sum(IEnumerable<Interval> source)
    {
        var result = default(Interval);
        foreach (var e in source)
        {
            if (result is null)
            {
                result = e;
            }
            else
            {
                result += e;
            }
        }

        return result ?? Default;
    }
};