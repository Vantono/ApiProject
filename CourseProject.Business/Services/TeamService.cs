using AutoMapper;
using CourseProject.Common.DTOs.Teams;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Business.Services;

public class TeamService : ITeamService
{
    private IGenericRepository<Team> TeamRepository { get; }
    private IGenericRepository<Employee> EmployeeRepository { get; }
    private IMapper Mapper { get; }

    public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository, IMapper mapper)
    {
        TeamRepository = teamRepository;
        EmployeeRepository = employeeRepository;
        Mapper = mapper;
    }


    public async Task<int> CreatTeamAsync(TeamCreate teamCreate)
    {
        Expression<Func<Employee, bool>> employeeFilter = (employee)=> teamCreate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
        var entity = Mapper.Map<Team>(teamCreate);
        entity.Employees= employees;
        await TeamRepository.InsertAsyinc(entity);
        await TeamRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        var entity = await TeamRepository.GetByIdAsync(teamDelete.Id);
        TeamRepository.Delete(entity);
        await TeamRepository.SaveChangesAsync();
    }

    public  async Task<TeamGet> GetTeamAsync(int Id)
    {
        var entity =  await TeamRepository.GetByIdAsync(Id, (team) => team.Employees);
        return Mapper.Map<TeamGet>(entity);
    }

    public async Task<List<TeamGet>> GetTeamsAsync()
    {
        var entities =await TeamRepository.GetAsync(null, null, (team)=> team.Employees);
        return Mapper.Map<List<TeamGet>>(entities);
    }

    public async Task UpdateteamAsync(TeamUpdate teamUpdate)
    {
        Expression<Func<Employee, bool>> employeeFilter = (employee) => teamUpdate.Employees.Contains(employee.Id);
        var employees = await EmployeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
        var existingEntity = await TeamRepository.GetByIdAsync(teamUpdate.Id, (team) => team.Employees);
        Mapper.Map(teamUpdate,existingEntity);
        existingEntity.Employees = employees;
        TeamRepository.Update(existingEntity);
        await TeamRepository.SaveChangesAsync();
        
    }
}
