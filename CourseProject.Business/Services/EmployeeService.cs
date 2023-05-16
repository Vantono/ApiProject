using AutoMapper;
using CourseProject.Business.Exceptions;
using CourseProject.Business.Validators;
using CourseProject.Common.DTOs.Employee;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Model;
using FluentValidation;
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
    private IGenericRepository<Job> JobRepository { get; }
    private IGenericRepository<Address> AddressRepository { get; }
    private IMapper Mapper { get; }
    private EmployeeCreateValidator EmployeeCreateValidator { get; }
    private EmployeeUpdateValidator EmployeeUpdateValidator { get; }
    private IFileService FileService { get; }
    private ImageFileValidator ImageFileValidator { get; }

    public EmployeeService(IGenericRepository<Employee> employeeRepository,IGenericRepository<Job> jobRepository,IGenericRepository<Address> addressRepository ,IMapper mapper,
        EmployeeCreateValidator employeeCreateValidator, EmployeeUpdateValidator employeeUpdateValidator, IFileService fileService, ImageFileValidator imageFileValidator)
    {
        EmployeeRepository = employeeRepository;
        JobRepository = jobRepository;
        AddressRepository = addressRepository;
        Mapper = mapper;
        EmployeeCreateValidator = employeeCreateValidator;
        EmployeeUpdateValidator = employeeUpdateValidator;
        FileService = fileService;
        ImageFileValidator = imageFileValidator;
    }


    public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
    {
        await EmployeeCreateValidator.ValidateAndThrowAsync(employeeCreate);

        var address = await AddressRepository.GetByIdAsync(employeeCreate.AddressId);
        if (address==null)
            throw new AddressNotFoundException(employeeCreate.AddressId);
       
        var job =  await JobRepository.GetByIdAsync(employeeCreate.JobId);
        if (job==null)
            throw new JobNotFoundException(employeeCreate.JobId);
        
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
        
        if (entity == null)
            throw new EmployeeNotFoundException(employeeDelete.Id);
       
        EmployeeRepository.Delete(entity);
        await EmployeeRepository.SaveChangesAsync();

    }

    public async Task<EmployeeDetails> GetEmployeeAsync(int id)
    {
        var entity =  await EmployeeRepository.GetByIdAsync(id, (employee) => employee.Address, (employee) => employee.Job, (employee) => employee.Teams);
        if (entity == null)
            throw new EmployeeNotFoundException(id);
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
        await EmployeeUpdateValidator.ValidateAndThrowAsync(employeeUpdate);

        var address = await AddressRepository.GetByIdAsync(employeeUpdate.AddressId);
        if (address == null)
            throw new AddressNotFoundException(employeeUpdate.AddressId);
        var job = await JobRepository.GetByIdAsync(employeeUpdate.JobId);
        if (job == null)
            throw new JobNotFoundException(employeeUpdate.JobId);
        
        var existingEmployee = await EmployeeRepository.GetByIdAsync(employeeUpdate.Id);
        if (existingEmployee == null)
            throw new EmployeeNotFoundException(employeeUpdate.Id);
        
        var entity = Mapper.Map<Employee>(employeeUpdate);
        entity.Address = address;
        entity.Job = job;
       

        
        EmployeeRepository.Update(entity);
        await EmployeeRepository.SaveChangesAsync();

    }

    public async Task UpdateProfilePhotoAsync(ProfilePhotoUpdate profilePhotoUpdate)
    {
        await ImageFileValidator.ValidateAndThrowAsync(profilePhotoUpdate.Photo);

        var employee = await EmployeeRepository.GetByIdAsync(profilePhotoUpdate.EmployeeId);

        if (employee == null)
            throw new EmployeeNotFoundException(profilePhotoUpdate.EmployeeId);
        if (employee.ProfilePhotoPath != null)
            FileService.DeleteFile(employee.ProfilePhotoPath);
        var fileName = await FileService.SaveFileAsync(profilePhotoUpdate.Photo);
        employee.ProfilePhotoPath = fileName;

        EmployeeRepository.Update(employee);
        await EmployeeRepository.SaveChangesAsync();
    }
}
