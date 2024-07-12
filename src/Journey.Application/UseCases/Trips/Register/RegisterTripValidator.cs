using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Register;

public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
{
    public RegisterTripValidator()
    {
        RuleFor(request => request.Name).NotEmpty()
            .WithMessage(ResourceErrorMessage.NAME_NULL_OR_EMPTY);
        RuleFor(request => request.StartDate.Date)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(ResourceErrorMessage.DATE_MUST_BE_LATER_THAN_TODAY);
        RuleFor(request => request)
            .Must(request => request.EndDate.Date >= request.StartDate.Date)
            .WithMessage(ResourceErrorMessage.END_DATE_MUST_BE_LATER_THAN_START_DATE);
    }
}


