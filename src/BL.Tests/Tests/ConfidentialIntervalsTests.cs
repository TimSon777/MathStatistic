using BL;
using Xunit;

namespace Tests.Tests;

public class ConfidentialIntervalsTests
{
    [Theory]
    [InlineData(0, 2, 7)]
    [InlineData(1, 25, 60)]
    public void Ordinary_GeneralMean(int indexDataSet, double expectedMinLeft, double expectedMaxRight)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic.WithAllParams(selection);

        Assert.True(statistic.GeneralMean.Left >= expectedMinLeft && statistic.GeneralMean.Right <= expectedMaxRight);
    }

    [Theory]
    [InlineData(0, 3, 30)]
    [InlineData(1, 200, 1600)]
    public void Ordinary_GeneralStandardDeviation(int indexDataSet, double expectedMinLeft, double expectedMaxRight)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic.WithAllParams(selection);

        Assert.True(statistic.GeneralVariance.Left >= expectedMinLeft &&
                    statistic.GeneralVariance.Right <= expectedMaxRight);
    }
}