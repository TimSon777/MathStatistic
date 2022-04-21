using System.Text;

namespace BL;

public static class StatisticReporter
{
    public static string WriteAsText(this Statistic statistic)
    {
        var intervalText = new StringBuilder();
        foreach (var interval in statistic.Intervals)
        {
            intervalText.Append("\t"+interval);
            intervalText.Append(Environment.NewLine);
        }

        return $"Minimum: {statistic.Min}{Environment.NewLine}" +
               $"Maximum: {statistic.Max}{Environment.NewLine}" +
               $"Interval Width: {statistic.IntervalWidth}{Environment.NewLine}" +
               $"Count Intervals: {statistic.Intervals.Count}{Environment.NewLine}" +
               $"Intervals:{Environment.NewLine}{intervalText}" +
               $"Mean: {statistic.Mean}{Environment.NewLine}" +
               $"Variance: {statistic.Variance}{Environment.NewLine}" +
               $"Standard Deviation: {statistic.StandardDeviation}{Environment.NewLine}" +
               $"Mode: {statistic.Mode}{Environment.NewLine}" +
               $"Asymmetry Coefficient: {statistic.AsymmetryCoefficient}{Environment.NewLine}" +
               $"Kurtosis Coefficient: {statistic.KurtosisCoefficient}{Environment.NewLine}" +
               WriteConclusionAsText(statistic);
    }

    public static string WriteConclusionAsText(this Statistic statistic)
    {
        var asymmetryCoefficientConclusion = new StringBuilder();
        switch (statistic.AsymmetryCoefficient)
        {
            case > 0:
                asymmetryCoefficientConclusion.Append($"The distribution is skewed to the right (Asymmetry coefficient > 0).{Environment.NewLine}");
                break;
            case < 0:
                asymmetryCoefficientConclusion.Append($"The distribution is skewed to the left (Asymmetry coefficient < 0).{Environment.NewLine}");
                break;
            case 0:
                asymmetryCoefficientConclusion.Append($"The distribution approximates the normal distribution (Asymmetry coefficient = 0).{Environment.NewLine}");
                break;
        }

        var kurtosisCoefficientConclusion = new StringBuilder();
        switch (statistic.KurtosisCoefficient)
        {
            case > 0:
                kurtosisCoefficientConclusion.Append($"The distribution is higher relative to the reference normal distribution (Kurtosis coefficient > 0).{Environment.NewLine}");
                break;
            case < 0:
                kurtosisCoefficientConclusion.Append($"The distribution is lower relative to the reference normal distribution (Kurtosis coefficient < 0).{Environment.NewLine}");
                break;
            case 0:
                kurtosisCoefficientConclusion.Append($"The distribution approximates the normal distribution (Kurtosis coefficient = 0).{Environment.NewLine}");
                break;
        }

        return asymmetryCoefficientConclusion + kurtosisCoefficientConclusion.ToString();
    }
}