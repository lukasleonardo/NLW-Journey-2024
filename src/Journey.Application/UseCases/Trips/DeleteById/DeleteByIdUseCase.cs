using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.DeleteById;

public class DeleteByIdUseCase
{
    public void Execute(Guid id)
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
        dbContext.Trips.Remove(response);
        dbContext.SaveChanges();
    }
}

