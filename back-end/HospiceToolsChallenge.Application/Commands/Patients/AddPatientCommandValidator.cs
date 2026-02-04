using FluentValidation;

namespace HospiceToolsChallenge.Application.Commands.Patients
{
    public class AddPatientCommandValidator : AbstractValidator<AddPatientCommand>
    {
        public AddPatientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name max lenght is 50")
                .MinimumLength(2).WithMessage("First name min lenght is 2");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name max lenght is 50")
                .MinimumLength(2).WithMessage("Last name min lenght is 2");

            RuleFor(x => x.Age)
                .LessThan(200).WithMessage("Age max value is 200")
                .GreaterThanOrEqualTo(0).WithMessage("Age min value is 0");
        }
    }
}
