namespace Services.ExceptionCraft;

public class AgeLessException: Exception 
{
    public AgeLessException(string message) : base(message)
    {
        
    }
}