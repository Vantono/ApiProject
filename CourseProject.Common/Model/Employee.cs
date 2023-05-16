using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Common.Model;

public class Employee : BaseEntity
{
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public Job Job { get; set; } = default!;
    public List<Team> Teams { get; set; } = default!;
    public string? ProfilePhotoPath { get; set; }
}
