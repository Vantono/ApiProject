using CourseProject.Common.DTOs.Employee;

namespace CourseProject.Common.DTOs.Teams;

public record TeamGet(int Id, string Name, List<EmployeeList> Employees);
