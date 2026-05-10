using Application.DTO.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class GetAllStudentsValidator : AbstractValidator<GetAllStudentsRequest>
    {
        public GetAllStudentsValidator()
        {
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("Search Age with Positive Number");
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Search page with Positive Number");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("Page Size must be positive");
        }
    }
}
