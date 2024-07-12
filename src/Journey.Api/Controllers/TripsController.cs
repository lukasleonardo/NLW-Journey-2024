using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.RegisterActivity;
using Journey.Application.UseCases.Trips.DeleteById;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]  
public class TripsController : ControllerBase  
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request) 
        {
        var useCase = new RegisterTripUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();
        var response = useCase.Execute();

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseTripJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson),StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var useCase = new GetTripByIdUseCase();
        var response = useCase.Execute(id);

        return Ok(response);

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult DeleteById([FromRoute]Guid id) 
    {
        var useCase = new DeleteByIdUseCase();
        useCase.Execute(id); 
        return NoContent();
    }


    [HttpPost("{tripId}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public IActionResult RegisterActivity(
        [FromRoute] Guid tripId,
        [FromBody] RequestRegisterActivityJson request)
    {
        var useCase = new RegisterActivityUseCase();
        var response = useCase.Execute(tripId, request);

        return Created(string.Empty,response);
    }


    [HttpPut("{tripId}/activity/{activityId}/completed")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        var useCase = new CompleteActivityUseCase();
        useCase.Execute(tripId, activityId);

        return NoContent();
    }

    [HttpDelete("{tripId}/activity/{activityId}/delete")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public IActionResult DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
    {
        var useCase = new DeleteActivityUseCase();
        useCase.Execute(tripId, activityId);

        return NoContent();
    }

}

     


