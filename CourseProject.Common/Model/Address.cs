using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Common.Model;

public class Address : BaseEntity
{
    public string Street { get; set; } = default!;
    public string Zipcode { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; }
    public List<Employee> Employees { get; set; } = default!;


}