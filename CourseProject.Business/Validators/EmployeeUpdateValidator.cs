using CourseProject.Common.DTOs.Address;
using CourseProject.Common.DTOs.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Validators;

public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
{
    public EmployeeUpdateValidator()
    {
        RuleFor(employeeUpdate => employeeUpdate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(employeeUpdate => employeeUpdate.LastName).NotEmpty().MaximumLength(50);
    }
}
