using Agenda.DTOs;
using FluentValidation;

namespace Agenda.Validations
{
    public class CreateContactValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty()
                        .WithMessage("The name must not be empty")
                    .NotNull()
                        .WithMessage("The name must not be null")
                    .MaximumLength(50)
                        .WithMessage("The maximun Length is 50");

            RuleFor(x => x.LastName)
                .NotEmpty()
                        .WithMessage("The LastName must not be empty")
                    .NotNull()
                        .WithMessage("The LastName must not be null")
                    .MaximumLength(50)
                        .WithMessage("The maximun Length is 50");

            RuleFor(x => x.Address)
                .MaximumLength(200)
                    .WithMessage("The maximun Length is 200");

            RuleFor(x => x.PhoneNumber)
                    .NotEmpty()
                        .WithMessage("The PhoneNumber must not be empty")
                    .NotNull()
                        .WithMessage("The PhoneNumber must not be null")
                    .MinimumLength(10)
                        .WithMessage("The minimun Length is 10")
                    .MaximumLength(12)
                        .WithMessage("The maximun Length is 12");
        }
    }
}
