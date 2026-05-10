using Application.DTO.Mark;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class RecordMarkValidator : AbstractValidator<RecordMarkRequest>
    {
        public RecordMarkValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Invalid Student Id.");
            RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("Invalid Class Id.");
            RuleFor(x => x.ExamMark).InclusiveBetween(0, 100).WithMessage("Exam mark must be between 0 and 100.");
            RuleFor(x => x.AssignmentMark).InclusiveBetween(0, 100).WithMessage("Assignment mark must be between 0 and 100.");
        }
    }
}
