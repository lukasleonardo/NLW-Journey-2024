using System.Net;

namespace Journey.Exception.ExceptionsBase;

public class ErrorOnValidationException : JourneyException
{   
    private readonly IList<string> _errors;
    public ErrorOnValidationException(IList<string> message) : base(string.Empty) 
    {
        _errors = message;
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }

    public override IList<string> GetErrorMessage()
    {
        return _errors;
    }
}

