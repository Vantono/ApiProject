using CourseProject.Common.DTOs.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Common.Interfaces;

public interface IJobService
{
    Task<int> CreateJobAsync(JobCreate jobCreate);
    Task UpdateJobAsync(JobUpdate jobUpdate);
    Task<List<JobGet>> GetJobsAsync();
    Task<JobGet> GetJobAsync(int Id);
    Task DeleteJobAsync(JobDelete jobDelete);
}
