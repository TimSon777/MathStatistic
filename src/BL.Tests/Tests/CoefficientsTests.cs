using BL;
using Xunit;

namespace Tests.Tests;

public class CoefficientsTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 0)]
    public void Ordinary_KurtosisCoefficient(int indexDataSet, double expectedMinOrdinaryGeneralKurtosisCoefficient, double expectedMaxOrdinaryGeneralKurtosisCoefficient)
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
        
        Assert.True(statistic.KurtosisCoefficient
            .Between(expectedMinOrdinaryGeneralKurtosisCoefficient, expectedMaxOrdinaryGeneralKurtosisCoefficient));
    }
    
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 0)]
    public void Ordinary_AsymmetryCoefficient(int indexDataSet, double expectedMinOrdinaryGeneralAsymmetryCoefficient, double expectedMaxOrdinaryGeneralAsymmetryCoefficient)
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
        
        Assert.True(statistic.AsymmetryCoefficient
            .Between(expectedMinOrdinaryGeneralAsymmetryCoefficient, expectedMaxOrdinaryGeneralAsymmetryCoefficient));
    }
}