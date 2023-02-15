using FluentValidation;
using KoboPay.Data.Models;
using KoboPay.Logic.Dtos;

namespace KoboPay.Logic.Helpers.Validations
{
    public class CourseValidation: AbstractValidator<CourseDto>
    {
        public CourseValidation()
        {
            RuleFor(x => x.CourseTitle)
                .NotEmpty()
                .WithMessage("Please provide course title");

            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .WithMessage("Must bind course to a department");

            RuleFor(x => x.CourseCode)
                .NotEmpty()
                .WithMessage("Please enter course code. E.g: ENG101");

            RuleFor(x => x.CourseLecturer)
                .NotEmpty()
                .WithMessage("Please select course lecturer.");
        }
    }

    //public class StudentCourseValidator : AbstractValidator<List<StudentCourseDto>>
    //{
    //    public StudentCourseValidator()
    //    {
    //        RuleFor(x => x.FirstOrDefault.CourseId)
    //            .NotEmpty()
    //            .NotEqual(Guid.Empty)
    //            .WithMessage("Please provide Course identifier");

    //        RuleFor(x => x.StudentId)
    //           .NotEmpty()
    //           .NotEqual(Guid.Empty)
    //           .WithMessage("Please provide student identifier");
    //    }
    //}
}
