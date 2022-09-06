namespace Services.ExceptionCraft;

public class IsKeyExistInDictionaryException: Exception 
{
    public IsKeyExistInDictionaryException(string message) : base(message)
    {
        
    }
}