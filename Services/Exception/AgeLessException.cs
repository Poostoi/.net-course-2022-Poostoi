namespace Services.Exception;

public class AgeLessException: System.Exception 
{
    public AgeLessException(string message) : base(message)
    {
        
    }
}