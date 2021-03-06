using System.Linq;
using BL;
using Xunit;

namespace Tests.Tests;

public class IntervalSeriesTests
{
    [Theory]
    [InlineData(0, 0, 9)]
    [InlineData(1, -10, 70)]
    public void Ordinary(int indexDataSet, double expectedMin, int expectedMax)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic.WithAllParams(selection);

        Assert.Equal(expectedMin, statistic.Min);
        Assert.Equal(expectedMax, statistic.Max);
        Assert.Equal(selection.Count, statistic.Intervals.Sum(interval => interval.Frequency));
    }
}