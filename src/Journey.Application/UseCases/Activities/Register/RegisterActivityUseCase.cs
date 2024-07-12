using FluentValidation.Results;
using Journey.Application.UseCases.Activities.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Activities.RegisterActivity;
public class RegisterActivityUseCase
{
    public ResponseActivityJson Execute(Guid tripId ,RequestRegisterActivityJson request)
    {
      

        var dbContext = new JourneyDbContext();
        var entity = dbContext.Trips.FirstOrDefault(x => x.Id == tripId);

        Validate(entity, request);


        var tripActivity = new Activity
        {
            Name = request.Name,
            Date = request.Date,
            TripId = tripId,
        };

        dbContext.Activities.Add(tripActivity);
        dbContext.SaveChanges();

        return new ResponseActivityJson
        {
            Status = (Communication.Enums.ActivityStatus)tripActivity.Status,
            Date = tripActivity.Date,
            Name = tripActivity.Name,
            Id = tripActivity.Id
        };
    }


    private void Validate(Trip? entity,RequestRegisterActivityJson request) 
    {
        var validate = new RegisterActivityValidator();
        var result = validate.Validate(request);

        if (entity is null)
        { throw new NotFoundException(ResourceErrorMessage.TRIP_NOT_FOUND); }

        if ((request.Date >= entity.StartDate && request.Date <= entity.EndDate)==false)
        {
            result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessage.DATE_OUT_OF_BOUND));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }

}
