namespace BL;

public record ConfidenceInterval(double Left, double Right, double Probability)
{
    public override string ToString() 
        => $"{Left}-{Right} with probability {Probability}";
}