using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is JourneyException)
        {
            var jorneyException = (JourneyException)context.Exception;

            context.HttpContext.Response.StatusCode = (int)jorneyException.GetStatusCode();
            var responseError = new ResponseErrorsJson(jorneyException.GetErrorMessage());
            context.Result = new ObjectResult(responseError);
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var responseError = new ResponseErrorsJson(new List<string> 
            { ResourceErrorMessage.UNKNOW_ERROR });
            context.Result = new ObjectResult(responseError);
        }
    }
}

