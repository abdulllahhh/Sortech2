using Application.DTO.Class;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateClassValidator : AbstractValidator<CreateClassRequest>
    {
        public CreateClassValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Class name is required.");
        }
    }
}
