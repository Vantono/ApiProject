using AutoMapper;
using AutoMapper.Configuration.Conventions;
using CourseProject.Common.DTOs;
using CourseProject.Common.DTOs.Address;
using CourseProject.Common.DTOs.Employee;
using CourseProject.Common.DTOs.Job;
using CourseProject.Common.DTOs.Teams;
using CourseProject.Common.Model;

namespace CourseProject.Business;

public class DTOEntityMapperProfile : Profile
{

    public DTOEntityMapperProfile()
    {
        CreateMap<AddressCreate, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<AddressUpdate, Address>();
        CreateMap<Address, AddressGet>();

        CreateMap<JobCreate, Job>()
            .ForMember(dest =>dest.Id, opt => opt.Ignore());
        CreateMap<JobUpdate, Job>();
        CreateMap<Job, JobGet>();

        CreateMap<EmployeeCreate, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Job, opt => opt.Ignore())
            .ForMember(dest => dest.Teams, opt => opt.Ignore());
        CreateMap<EmployeeUpdate, Employee>()
            .ForMember(dest => dest.Job, opt => opt.Ignore())
            .ForMember(dest => dest.Teams, opt => opt.Ignore());
        CreateMap<Employee, EmployeeDetails>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Job, opt => opt.Ignore())
            .ForMember(dest => dest.Teams, opt => opt.Ignore())
            .ForMember(dest=> dest.Address, opt=> opt.Ignore());
        CreateMap<EmployeeList, Employee>();

        CreateMap<TeamCreate, Team>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Employees, opt => opt.Ignore());

        CreateMap<TeamUpdate, Team>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore());
        CreateMap<Team, TeamGet>();
            

        



    }
}