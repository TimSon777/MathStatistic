namespace BL;

public record Interval(double Left, double Right, int Frequency)
{
    public double Middle => (Left + Right) / 2;
};