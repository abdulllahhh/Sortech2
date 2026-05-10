using Application.DTO.Class;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class GetAverageMarksValidator : AbstractValidator<GetAverageMarksRequest>
    {
        public GetAverageMarksValidator()
        {
            RuleFor(x => x.ClassId).GreaterThan(0).WithMessage("Class Id Must Be Positive");
        }
    }
}
