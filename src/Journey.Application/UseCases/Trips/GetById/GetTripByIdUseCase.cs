using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById;

public class GetTripByIdUseCase
{
    public ResponseTripJson Execute(Guid id) 
    {
        var dbContext = new JourneyDbContext();
        var response = dbContext
            .Trips
            .Include(x => x.Activities)
            .FirstOrDefault(x => x.Id == id);
        
        if (response is null) 
        {
            throw new NotFoundException(ResourceErrorMessage.TRIP_NOT_FOUND);
        }

        return new ResponseTripJson 
        { 
            Id = response.Id,
            Name = response.Name,
            EndDate = response.EndDate,
            StartDate = response.StartDate,
            Activities = response.Activities.Select(x => new ResponseActivityJson 
            {
                Id = x.Id,
                Name = x.Name,
                Date = x.Date,
                Status = (Communication.Enums.ActivityStatus)x.Status

            }).ToList(),

        };
    }
}


