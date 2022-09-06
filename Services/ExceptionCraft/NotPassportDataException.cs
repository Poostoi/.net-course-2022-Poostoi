namespace Services.ExceptionCraft;

public class NotPassportDataException: Exception

{
    public NotPassportDataException(string message) : base(message)
    {
        
    }
}