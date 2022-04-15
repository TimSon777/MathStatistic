﻿using static System.Math;
using BL;
using Xunit;

namespace Tests;

public class SampleParametersTests
{
    [Theory]
    [InlineData(0, 4, 5)]
    [InlineData(1, 35, 45)]
    public void Ordinary_Mean(int indexDataSet, double expectedMinAverage, double expectedMaxAverage)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = new Statistic(selection)
            .WithMin()
            .WithMax()
            .WithSturgess()
            .WithIntervalWidth()
            .WithIntervals()
            .WithMean();
        
        Assert.True(expectedMinAverage < statistic.Mean && statistic.Mean < expectedMaxAverage);
    }
    
    [Theory]
    [InlineData(0, 7, 9)]
    [InlineData(1, 400, 500)]
    public void Ordinary_VarianceAndStandardDeviation(int indexDataSet, double expectedMinVariance, double expectedMaxVariance)
    {
        var selection = Data.Sets[indexDataSet];
        var statistic = new Statistic(selection)
            .WithMin()
            .WithMax()
            .WithSturgess()
            .WithIntervalWidth()
            .WithIntervals()
            .WithMean()
            .WithVariance()
            .WithStandardDeviation();
        
        Assert.True(statistic.Variance.Between(expectedMinVariance, expectedMaxVariance));
        Assert.True(statistic.StandardDeviation.Between(Sqrt(expectedMinVariance), Sqrt(expectedMaxVariance)));
    }
}