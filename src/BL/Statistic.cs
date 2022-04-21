using static System.Math;

namespace BL;

public class Statistic
{
    public readonly double[] OrderedSelection;
    public IReadOnlyCollection<Interval> Intervals { get; private set; } = new List<Interval>();

    public double Min { get; private set; }
    public double Max { get; private set; }

    public readonly int ElementsCount;

    public double Mean { get; private set; }
    public double StandardDeviation { get; private set; }
    public double IntervalWidth { get; private set; }
    public int PreCountIntervals { get; private set; }
    public double Variance { get; private set; }
    public double Mode { get; private set; }
    public double Median { get; private set; }
    public double AsymmetryCoefficient { get; private set; }
    public double KurtosisCoefficient { get; private set; }

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
        var numberIntervals = 1 + 3.322 * Log10(ElementsCount);
        PreCountIntervals = (int) Ceiling(numberIntervals);
        return this;
    }

    public Statistic WithIntervalWidth()
    {
        if (Abs(Min - Max) < .000001)
        {
            Min -= .0000001;
        }
        
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
        Variance = Intervals.Sum(interval => (interval.Middle - Mean).Sqr() * interval.Frequency) /
                   OrderedSelection.Length;
        return this;
    }

    public Statistic WithStandardDeviation()
    {
        StandardDeviation = Sqrt(Variance);
        return this;
    }

    public Statistic WithMode()
    {
        var startIndexOfMaxFrequency = 0;
        var maxFrequency = Intervals.First().Frequency;
        var maxFrequencies = new List<Interval>();

        for (var i = 0; i < Intervals.Count; i++)
        {
            var interval = Intervals.ElementAt(i);
            var frequency = interval.Frequency;

            if (frequency == maxFrequency)
            {
                maxFrequencies.Add(interval);
            }
            else if (frequency > maxFrequency)
            {
                maxFrequencies = new List<Interval> {interval};
                maxFrequency = frequency;
                startIndexOfMaxFrequency = i;
            }
        }

        var endIndexOfMaxFrequency = startIndexOfMaxFrequency + maxFrequencies.Count - 1;

        var previousIntervals = maxFrequencies.Count >= startIndexOfMaxFrequency
            ? Intervals
                .Take(startIndexOfMaxFrequency)
            : Intervals
                .Take(startIndexOfMaxFrequency)
                .TakeLast(maxFrequencies.Count);

        var previousIntervalFrequency = previousIntervals.Sum(interval => interval.Frequency);

        var nextIntervals = maxFrequencies.Count > Intervals.Count - endIndexOfMaxFrequency
            ? Intervals
                .Skip(endIndexOfMaxFrequency + 1)
            : Intervals
                .Skip(endIndexOfMaxFrequency + 1)
                .Take(maxFrequencies.Count);
        
        var nextIntervalFrequency = nextIntervals.Sum(interval => interval.Frequency);

        var modeInterval = Interval.Sum(maxFrequencies);

        Mode = modeInterval.Left + (modeInterval.Frequency - previousIntervalFrequency) / (double)
            (2 * modeInterval.Frequency - previousIntervalFrequency - nextIntervalFrequency) * modeInterval.Length;

        return this;
    }

    public Statistic WithMedian()
    {
        var accumulatedFrequency = 0;
        var first = Interval.Default;
        var second = Interval.Default;
        var maxAccumulatedFrequency = OrderedSelection.Length / (double) 2;
        foreach (var e in Intervals)
        {
            if (accumulatedFrequency >= maxAccumulatedFrequency)
            {
                second = e;
                break;
            }
            first = e;
            accumulatedFrequency += e.Frequency;
        }

        var interval = Abs(accumulatedFrequency - maxAccumulatedFrequency) < 0.00001
            ? first + second 
            : first;

        var accumulatedFrequencyPrevious = accumulatedFrequency - interval.Frequency;
        Median = interval.Left + (OrderedSelection.Length * 0.5 - accumulatedFrequencyPrevious) * 
            interval.Length / interval.Frequency;

        return this;
    }

    private double WithCentralMoment(int order) => 
        Intervals.Sum(interval => (interval.Middle - Mean).Pow(order) * interval.GetRelativeFrequency(ElementsCount));
    
    public Statistic WithAsymmetryCoefficient()
    {
        var standardDeviationCub = StandardDeviation.Cub();
        AsymmetryCoefficient = standardDeviationCub != 0 
            ?  WithCentralMoment(3) / StandardDeviation.Cub()
            : 0;
        return this;
    }

    public Statistic WithKurtosisCoefficient()
    {
        var varianceSqr = Variance.Sqr();
        KurtosisCoefficient = varianceSqr != 0 
            ? WithCentralMoment(4) / Variance.Sqr() - 3 
            : 0;
        
        return this;
    }

    public static Statistic WithAllParams(IReadOnlyCollection<double> selection)
    {
        return CreateBy(selection)
            .WithMin()
            .WithMax()
            .WithSturgess()
            .WithIntervalWidth()
            .WithIntervals()
            .WithMean()
            .WithVariance()
            .WithStandardDeviation()
            .WithMode()
            .WithMedian()
            .WithKurtosisCoefficient()
            .WithAsymmetryCoefficient();
    }
}