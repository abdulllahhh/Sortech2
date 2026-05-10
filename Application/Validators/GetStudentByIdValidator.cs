using Application.DTO.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class GetStudentByIdValidator : AbstractValidator<GetStudentByIdRequest>
    {
        public GetStudentByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id Cannot Be negative");
        }
    }
}
