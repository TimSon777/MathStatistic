using BL;
using Xunit;

namespace Tests.Tests;

public class ConfidenceIntervalTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 0)]
    public void Ordinary_GeneralMean(int indexDataSet, double expectedMinOrdinaryGeneralMean, double expectedMaxOrdinaryGeneralMean)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic
            .CreateBy(selection)
            .WithMin()
            .WithMax()
            .WithSturgess()
            .WithIntervalWidth()
            .WithIntervals();
            //TODO
        
        Assert.True(statistic.GeneralMean.Left >= expectedMinOrdinaryGeneralMean 
                    && statistic.GeneralMean.Right <= expectedMaxOrdinaryGeneralMean);
    }
    
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 0)]
    public void Ordinary_GeneralVariance(int indexDataSet, double expectedMinOrdinaryGeneralVariance, double expectedMaxOrdinaryGeneralVariance)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic
            .CreateBy(selection)
            .WithMin()
            .WithMax()
            .WithSturgess()
            .WithIntervalWidth()
            .WithIntervals();
        //TODO
        
        Assert.True(statistic.GeneralVariance.Left >= expectedMinOrdinaryGeneralVariance 
                    && statistic.GeneralVariance.Right <= expectedMaxOrdinaryGeneralVariance);
    }
}