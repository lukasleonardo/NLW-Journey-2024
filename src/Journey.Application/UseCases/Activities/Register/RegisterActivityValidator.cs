using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Register;

public class RegisterActivityValidator:AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(request => request.Name).NotEmpty()
            .WithMessage(ResourceErrorMessage.NAME_NULL_OR_EMPTY);
    }
}

