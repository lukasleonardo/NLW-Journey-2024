using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activities.Complete;
public class CompleteActivityUseCase
{

    public void Execute(Guid tripId, Guid activityId)
    {
        var dbContext = new JourneyDbContext();
        var entity = dbContext.Activities.FirstOrDefault(x => x.Id == activityId && tripId == x.TripId);

        if (entity is null) 
        {
            throw new NotFoundException(ResourceErrorMessage.ACTIVITY_NOT_FOUND);
        }


        entity.Status = ActivityStatus.Done;
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }


}