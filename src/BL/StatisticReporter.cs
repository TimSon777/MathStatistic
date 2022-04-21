﻿using System.Text;

namespace BL;

public static class StatisticReporter
{
    public static string WriteAsText(this Statistic statistic)
    {
        var intervalText = new StringBuilder();
        foreach (var interval in statistic.Intervals)
        {
            intervalText.Append('\n');
            intervalText.Append(interval);
        }
        
        return $"Minimum: {statistic.Min}{Environment.NewLine}" +
               $"Maximum: {statistic.Max}{Environment.NewLine}" +
               $"Interval Width: {statistic.IntervalWidth}{Environment.NewLine}" +
               $"Count Intervals: {statistic.Intervals.Count}{Environment.NewLine}" +
               $"Intervals: {intervalText}{Environment.NewLine}" +
               $"Mean: {statistic.Mean}{Environment.NewLine}" +
               $"Variance: {statistic.Variance}{Environment.NewLine}" +
               $"Standard Deviation: {statistic.StandardDeviation}{Environment.NewLine}" +
               $"Mode: {statistic.Mode}{Environment.NewLine}" +
               $"Asymmetry Coefficient: {statistic.AsymmetryCoefficient}{Environment.NewLine}" +
               $"Kurtosis Coefficient{statistic.KurtosisCoefficient}{Environment.NewLine}" +
               $"General Mean: {statistic.GeneralMean}{Environment.NewLine}" +
               $"General Variance: {statistic.GeneralVariance}";
    }
}