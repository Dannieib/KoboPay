using FluentValidation;
using KoboPay.Logic.Dtos;

namespace KoboPay.Logic.Helpers.Validations
{
    public class StudentValidation : AbstractValidator<CreateUpdateStudentDto>
    {
        public StudentValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Please, enter your first name");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Please, enter your last name");

            RuleFor(x => x.Age)
                .LessThanOrEqualTo(0)
                .WithMessage("Please, enter your age");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Please, enter a username");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Please, enter a unique email address");

            RuleFor(x => x.DepartmentId)
            .NotEqual(Guid.Empty)
            .WithMessage("Please, Select a department. Department identifier cannot be 0");
        }
    }
}