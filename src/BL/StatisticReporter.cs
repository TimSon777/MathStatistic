namespace BL;

public static class StatisticReporter
{
    public static List<string> WriteConclusionAsText(this Statistic statistic)
    {
        var asymmetryCoefficientConclusion = statistic.AsymmetryCoefficient switch
        {
            > 0 => "The distribution is skewed to the right (Asymmetry coefficient > 0).",
            < 0 => "The distribution is skewed to the left (Asymmetry coefficient < 0).",
            _ => "The distribution approximates the normal distribution (Asymmetry coefficient = 0)."
        };

        var kurtosisCoefficientConclusion = statistic.KurtosisCoefficient switch
        {
            > 0 => "The distribution is higher relative to the reference normal distribution (Kurtosis coefficient > 0).",
            < 0 => "The distribution is lower relative to the reference normal distribution (Kurtosis coefficient < 0).",
            _ => "The distribution approximates the normal distribution (Kurtosis coefficient = 0)."
        };

        var result = new List<string>
        {
            asymmetryCoefficientConclusion,
            kurtosisCoefficientConclusion
        };
        
        return result;
    }
}