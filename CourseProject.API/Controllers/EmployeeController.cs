﻿using CourseProject.Common.DTOs.Employee;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controllers;
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private IEmployeeService EmployeeService { get; }
    public EmployeeController(IEmployeeService employeeService)
    {
        EmployeeService = employeeService;
    }

    [HttpPost]
    [Route("Create")]
     public async Task<IActionResult> CreateEmployee(EmployeeCreate employeeCreate)
    {
        var Id = await EmployeeService.CreateEmployeeAsync(employeeCreate);
        return Ok(Id);
    }
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateEmployee(EmployeeUpdate employeeUpdate)
    {
        await EmployeeService.UpdateEmployeeAsync(employeeUpdate);
        return Ok();
    }
    [HttpPut]
    [Route("Update/ProfilePhoto")]
    public async Task<IActionResult> UpdateProfilePhoto([FromForm]ProfilePhotoUpdate profilePhotoUpdate)
    {
        await EmployeeService.UpdateProfilePhotoAsync(profilePhotoUpdate);
        return Ok();
    }
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteEmployee(EmployeeDelete employeeDelete)
    {
        await EmployeeService.DeleteEmployeeAsync(employeeDelete);
        return Ok();
    }
    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await EmployeeService.GetEmployeeAsync(id);
        return Ok(employee);
    }
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetEmployees([FromQuery]EmployeesFilter employeesFilter)
    {
        var employees = await EmployeeService.GetEmployeesAsync(employeesFilter);
        return Ok(employees);
    }
}
