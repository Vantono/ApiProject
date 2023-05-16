using Microsoft.AspNetCore.Http;

namespace CourseProject.Common.DTOs.Employee;

public record ProfilePhotoUpdate(int EmployeeId, IFormFile Photo);

