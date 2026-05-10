using Application.DTO.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class DeleteStudentValidator : AbstractValidator<DeleteStudentRequest>
    {
        public DeleteStudentValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id Cannot Be Empty");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id Cannot negattive");

        }
    }
}
