namespace BL.Exceptions;

public class NoElementsException : Exception
{
    public NoElementsException(string? paramName = null) : base(paramName)
    { }
}