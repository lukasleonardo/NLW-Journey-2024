using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Activities.Delete;
public class DeleteActivityUseCase
{
    public void Execute(Guid tripId, Guid activityId)
    {
        var dbContext = new JourneyDbContext();
        var entity = dbContext.Activities.FirstOrDefault(x => x.Id == activityId && tripId == x.TripId);

        if (entity is null)
        {
            throw new NotFoundException(ResourceErrorMessage.ACTIVITY_NOT_FOUND);
        }

        dbContext.Remove(entity);
        dbContext.SaveChanges();
    }
}