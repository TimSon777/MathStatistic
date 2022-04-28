using BL;
using Xunit;

namespace Tests.Tests;

public class AverageParametersTests
{
    [Theory]
    [InlineData(0, 3, 5)]
    [InlineData(1, 43, 50)]
    public void Ordinary_Mode(int indexDataSet, double expectedMinMode, double expectedMaxMode)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic.WithAllParams(selection);
        
        Assert.True(statistic.Mode.Between(expectedMinMode, expectedMaxMode));
    }
    
    [Theory]
    [InlineData(0, 4, 5)]
    [InlineData(1, 43, 48)]
    public void Ordinary_Median(int indexDataSet, double expectedMinMedian, double expectedMaxMedian)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic.WithAllParams(selection);
        
        Assert.True(statistic.Median.Between(expectedMinMedian, expectedMaxMedian));
    }
}