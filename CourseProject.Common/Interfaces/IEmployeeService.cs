﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseProject.Common.DTOs.Employee;

namespace CourseProject.Common.Interfaces;

public interface IEmployeeService
{
    Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
    Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
    Task<List<EmployeeList>> GetEmployeesAsync(EmployeesFilter employeesFilter);
    Task<EmployeeDetails> GetEmployeeAsync(int Id);
    Task DeleteEmployeeAsync(EmployeeDelete emplyeeDelete);

}