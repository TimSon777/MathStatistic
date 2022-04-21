namespace UI;

public static class Constants
{
    public static Font MicrosoftSansSerif(float fontSize = 10F) 
        => new("Microsoft Sans Serif", fontSize);

    public static readonly Size ButtonSize = new(300, 50);

    public static readonly Size FormDefaultSize = new(1200, 800);

    public static readonly Size TextBoxDefaultSize = new(1000, 200);
    
    public const int DefaultMargin = 3;
}