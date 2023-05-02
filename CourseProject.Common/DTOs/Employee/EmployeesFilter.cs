namespace CourseProject.Common.DTOs.Employee;

public record EmployeesFilter(string? FirstName, string LastName, string? Job,string Team, int? Skip, int Take);
