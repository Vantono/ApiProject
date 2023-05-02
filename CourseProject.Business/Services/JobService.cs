using AutoMapper;
using CourseProject.Common.DTOs;
using CourseProject.Common.DTOs.Job;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Model;

namespace CourseProject.Business.Services;

public class JobService : IJobService
{
    private IMapper Mapper { get; }
    private IGenericRepository<Job> JobRepository { get; }

    public JobService(IMapper mapper, IGenericRepository<Job> jobRepository)
    {
        Mapper = mapper;
        JobRepository = jobRepository;
    }
    public async Task<int> CreateJobAsync(JobCreate jobCreate)
    {
        var entity = Mapper.Map<Job>(jobCreate);
        await JobRepository.InsertAsyinc(entity);
        await JobRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteJobAsync(JobDelete jobDelete)
    {
        var entity = await JobRepository.GetByIdAsync(jobDelete.id);
        JobRepository.Delete(entity);
        await JobRepository.SaveChangesAsync();
    }

    public async Task<JobGet> GetJobAsync(int id)
    {
        var entity = await JobRepository.GetByIdAsync(id);
        return Mapper.Map<JobGet>(entity);
    }

    public async Task<List<JobGet>> GetJobsAsync()
    {
        var entities = await JobRepository.GetAsync(null, null);
        return Mapper.Map<List<JobGet>>(entities);
    }

    public async Task UpdateJobAsync(JobUpdate jobUpdate)
    {
        var entity = Mapper.Map<Job>(jobUpdate);
        JobRepository.Update(entity);
        await JobRepository.SaveChangesAsync();
    }
}
