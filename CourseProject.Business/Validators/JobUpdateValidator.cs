using CourseProject.Common.DTOs.Address;
using CourseProject.Common.DTOs.Job;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Validators;

public class JobUpdateValidator : AbstractValidator<JobUpdate>
{
    public JobUpdateValidator()
    {
        RuleFor(jobUpdate => jobUpdate.Name).NotEmpty().MaximumLength(100);
        RuleFor(jobUpdate => jobUpdate.Description).NotEmpty().MaximumLength(100);
        
    }
}
