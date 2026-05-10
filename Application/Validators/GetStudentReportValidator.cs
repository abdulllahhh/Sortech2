using Application.DTO.StudentReport;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class GetStudentReportValidator : AbstractValidator<GetReportRequest>
    {
        public GetStudentReportValidator()
        {
            RuleFor(x => x.StudentId).GreaterThan(0).WithMessage("Student Id Must Be Positive");

        }
    }
}
