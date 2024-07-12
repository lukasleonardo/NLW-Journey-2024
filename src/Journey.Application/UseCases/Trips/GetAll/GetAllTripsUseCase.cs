using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.GetAll;

public class GetAllTripsUseCase
{
    public ResponseTripsJson Execute()
    {
        var dbContext = new JourneyDbContext();

        var trips = dbContext.Trips.ToList();

        return new ResponseTripsJson
        {
            Trips = trips.Select(x => new ResponseShortTripJson
            {
                StartDate = x.StartDate,
                Name = x.Name,
                EndDate = x.EndDate,
                Id = x.Id   
            }).ToList(),


        };   
        
    }

}

