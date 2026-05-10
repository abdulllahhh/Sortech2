using Application.DTO.Class;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class DeleteClassValidator : AbstractValidator<DeleteClassRequest>
    {
        public DeleteClassValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Class Id Must Be Positive");
        }
    }
}
