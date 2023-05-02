using AutoMapper;
using CourseProject.Common.DTOs.Employee;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Services;

public class EmployeeService : IEmployeeService
{
    private IGenericRepository<Employee> EmployeeRepository { get; }
    public IGenericRepository<Job> JobRepository { get; }
    public IGenericRepository<Address> AddressRepository { get; }
    private IMapper Mapper { get; }

    public EmployeeService(IGenericRepository<Employee> employeeRepository,IGenericRepository<Job> jobRepository,IGenericRepository<Address> addressRepository ,IMapper mapper)
    {
        EmployeeRepository = employeeRepository;
        JobRepository = jobRepository;
        AddressRepository = addressRepository;
        Mapper = mapper;
    }


    public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
    {
        var address = await AddressRepository.GetByIdAsync(employeeCreate.AddressId);
        var job =  await JobRepository.GetByIdAsync(employeeCreate.JobId);
        var entity = Mapper.Map<Employee>(employeeCreate);
        entity.Address = address;
        entity.Job = job;
        await EmployeeRepository.InsertAsyinc(entity);
        await EmployeeRepository.SaveChangesAsync();
        return entity.Id;
    }

    public  async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
    {
        var entity = await EmployeeRepository.GetByIdAsync(employeeDelete.Id);
        EmployeeRepository.Delete(entity);
        await EmployeeRepository.SaveChangesAsync();

    }

    public async Task<EmployeeDetails> GetEmployeeAsync(int id)
    {
        var entity =  await EmployeeRepository.GetByIdAsync(id, (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);
        return Mapper.Map<EmployeeDetails>(entity);
    }

    public async Task<List<EmployeeList>> GetEmployeesAsync(EmployeesFilter employeesFilter)
    {
        Expression<Func<Employee, bool>> firstNameFilter = (employee)=> employeesFilter.FirstName == null? true:
        employee.Firstname.StartsWith(employeesFilter.FirstName);
        Expression<Func<Employee, bool>> lastNameFilter = (employee) => employeesFilter.LastName == null ? true :
        employee.Lastname.StartsWith(employeesFilter.LastName);
        Expression<Func<Employee, bool>> jobFilter = (employee) => employeesFilter.Job == null ? true :
        employee.Job.Name.StartsWith(employeesFilter.Job);
        Expression<Func<Employee, bool>> teamFilter = (employee) => employeesFilter.Team == null ? true :
        employee.Teams.Any(team => team.Name.StartsWith(employeesFilter.Team));

        var entities = await EmployeeRepository.GetFilteredAsync(new Expression<Func<Employee, bool>>[]
        {
            firstNameFilter, lastNameFilter, jobFilter, teamFilter
        }, 
        employeesFilter.Skip, employeesFilter.Take,
        (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);
        return Mapper.Map<List<EmployeeList>>(entities);
    }

    public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
    {
        var address = await AddressRepository.GetByIdAsync(employeeUpdate.AddressId);
        var job = await JobRepository.GetByIdAsync(employeeUpdate.JobId);
        var entity = Mapper.Map<Employee>(employeeUpdate);
        entity.Address = address;
        entity.Job = job;
        EmployeeRepository.Update(entity);
        await EmployeeRepository.SaveChangesAsync();

    }
}
