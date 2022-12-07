using Dictionary.Common.Models.RequestModels;
using FluentValidation;

namespace Dictionary.Api.Application.Features.Commands.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(s => s.EmailAddress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");

            RuleFor(s => s.Password)
                .NotNull()
                .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLenght} characters");
        }
    }
}
