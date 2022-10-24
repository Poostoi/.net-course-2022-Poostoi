namespace Services.ExceptionCraft;

public class DoesNotExistException:Exception
{
    public DoesNotExistException(string message) : base(message)
    {
        
    }
}