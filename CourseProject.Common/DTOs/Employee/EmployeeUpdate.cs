using CourseProject.Common.DTOs.Job;

namespace CourseProject.Common.DTOs.Employee;

public record EmployeeUpdate (int Id, string FirstName, string LastName,int AddressId,int JobId);
