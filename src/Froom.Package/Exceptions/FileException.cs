namespace Froom.Package.Exceptions;

public class FileException : Exception
{
    public FileException(string message)
    {
        Message = message;
    }

    public override string Message { get; }
}