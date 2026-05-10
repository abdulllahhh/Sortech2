using Application.DTO.Class;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class GetAllClasseValidator : AbstractValidator<GetAllClassesRequest>
    {
        public GetAllClasseValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page Number Must Be Greater Than 0");
        }
    }
}
