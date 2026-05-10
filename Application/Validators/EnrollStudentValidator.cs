using Application.DTO.Enrollment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class EnrollStudentValidator : AbstractValidator<EnrollStudentRequest>
    {
        public EnrollStudentValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Invalid Student Id.");
            RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("Invalid Class Id.");
        }
    }
}
