using CourseProject.Common.DTOs.Address;
using CourseProject.Common.DTOs.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Validators;

public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
{
    public EmployeeCreateValidator()
    {
        RuleFor(employeeCreate => employeeCreate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(employeeCreate => employeeCreate.LastName).NotEmpty().MaximumLength(50);
        
    }
}
