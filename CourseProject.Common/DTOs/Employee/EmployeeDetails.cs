using CourseProject.Common.DTOs.Job;
using CourseProject.Common.DTOs.Teams;

namespace CourseProject.Common.DTOs.Employee;

public record EmployeeDetails(int Id, string FirstName, string LastName, AddressGet Address, JobGet Job/* ,List<TeamGet> Teams*/);
