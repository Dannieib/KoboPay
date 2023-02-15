using FluentValidation;
using KoboPay.Logic.Dtos;

namespace KoboPay.Logic.Helpers.Validations
{
    public class DepartmentValidation : AbstractValidator<CreateUpdateDepartment>
    {
        public DepartmentValidation()
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty()
                .WithMessage("Department name cannot be empty. E.g: Computer Science");
        }
    }
}
