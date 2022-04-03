namespace BL;

public class Statistic
{
    public IReadOnlyCollection<Interval> Intervals => _intervals;

    private readonly Interval[] _intervals;
    public Statistic(IEnumerable<double> selection)
    {
        _intervals = default;
    }

    public double Min { get; set; }
    public double Max { get; set; }
    public int Count => _intervals.Length;
}