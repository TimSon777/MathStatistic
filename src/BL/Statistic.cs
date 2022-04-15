using static System.Math;

namespace BL;

public class Statistic
{
    public readonly double[] OrderedSelection;
    public IReadOnlyCollection<Interval> Intervals { get; private set; } = new List<Interval>();

    public double Min { get; private set; }
    public double Max { get; private set; }

    public readonly int ElementsCount;

    public double Mean { get; set; }
    public double StandardDeviation { get; set; }
    public double IntervalWidth { get; set; }
    public int PreCountIntervals { get; set; }
    public double Variance { get; set; }

    private Statistic(IReadOnlyCollection<double> selection)
    {
        OrderedSelection = selection
            .OrderBy(e => e)
            .ToArray();
        
        ElementsCount = selection.Count;
    }

    public static Statistic CreateBy(IReadOnlyCollection<double> selection)
    {
        return new Statistic(selection);
    }

    public Statistic WithMin()
    {
        Min = OrderedSelection.Min();
        return this;
    }
    
    public Statistic WithMax()
    {
        Max = OrderedSelection.Max();
        return this;
    }

    public Statistic WithSturgess()
    {
        var numberIntervals = 1 + 3.322 * Math.Log10(ElementsCount);
        PreCountIntervals = (int) Math.Ceiling(numberIntervals);
        return this;
    }

    public Statistic WithIntervalWidth()
    {
        IntervalWidth = (Max - Min) / PreCountIntervals;
        return this;
    }

    public Statistic WithIntervals(int delta = 10)
    {
        var start = Min - IntervalWidth / delta;
        var j = 0;
        var list = new List<Interval>();
        do
        {
            var end = start + IntervalWidth;
            var frequency = 0;
            
            while (OrderedSelection[j] >= start && OrderedSelection[j] < end)
            {
                frequency++;
                j++;
                
                if (j == ElementsCount)
                {
                    break;
                }
            }
            
            list.Add(new Interval(start, end, frequency));
            start = end;
        } while (start <= Max && j < ElementsCount);
        
        Intervals = list;
        return this;
    }

    public Statistic WithMean()
    {
        Mean = Intervals.Sum(interval => interval.Middle * interval.Frequency) / OrderedSelection.Length;
        return this;
    }

    public Statistic WithVariance()
    {
        Variance = Intervals.Sum(interval => (interval.Middle - Mean).Sqr() * interval.Frequency) / OrderedSelection.Length;
        return this;
    }

    public Statistic WithStandardDeviation()
    {
        StandardDeviation = Sqrt(Variance);
        return this;
    }
}
