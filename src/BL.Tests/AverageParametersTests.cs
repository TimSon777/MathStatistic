using BL;
using Xunit;

namespace Tests;

public class AverageParametersTests
{
    [Theory]
    [InlineData(0, 3, 5)]
    [InlineData(1, 43, 50)]
    public void Ordinary_Mode(int indexDataSet, double expectedMinMode, double expectedMaxMode)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic
            .CreateBy(selection)
            .WithMin()
            .WithMax()
            .WithSturgess()
            .WithIntervalWidth()
            .WithIntervals()
            .WithMode();
        
        Assert.True(statistic.Mode.Between(expectedMinMode, expectedMaxMode));
    }
}