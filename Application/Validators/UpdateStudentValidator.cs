using Application.DTO.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age must be a positive integer.");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid Student Id.");
        }
    }
}
