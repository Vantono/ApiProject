using CourseProject.Common.DTOs.Address;
using CourseProject.Common.DTOs.Job;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Validators;

public class JobCreateValidator : AbstractValidator<JobCreate>
{
    public JobCreateValidator()
    {
        RuleFor(jobCreate => jobCreate.Name).NotEmpty().MaximumLength(100);
        RuleFor(jobCreate => jobCreate.Description).NotEmpty().MaximumLength(100);

    }
}