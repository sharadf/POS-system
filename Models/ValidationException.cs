using POS_system.Models;

public class ValidationException : Exception
{
    public readonly List <ValidationResponse> validationResponses;
    public ValidationException()
    {
        validationResponses = new List<ValidationResponse>();
    }
}