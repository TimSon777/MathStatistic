using BL;
using Xunit;

namespace Tests.Tests;

public class CoefficientsTests
{
    [Theory]
    [InlineData(1, -0.5, 0)]
    public void Ordinary_KurtosisCoefficient(int indexDataSet, double expectedMinOrdinaryGeneralKurtosisCoefficient, double expectedMaxOrdinaryGeneralKurtosisCoefficient)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic
            .WithAllParams(selection);
        
        Assert.True(statistic.KurtosisCoefficient
            .Between(expectedMinOrdinaryGeneralKurtosisCoefficient, expectedMaxOrdinaryGeneralKurtosisCoefficient));
    }
    
    [Theory]
    [InlineData(1, -1, 0)]
    public void Ordinary_AsymmetryCoefficient(int indexDataSet, double expectedMinOrdinaryGeneralAsymmetryCoefficient, double expectedMaxOrdinaryGeneralAsymmetryCoefficient)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = Statistic
            .WithAllParams(selection);
        
        Assert.True(statistic.AsymmetryCoefficient
            .Between(expectedMinOrdinaryGeneralAsymmetryCoefficient, expectedMaxOrdinaryGeneralAsymmetryCoefficient));
    }
}