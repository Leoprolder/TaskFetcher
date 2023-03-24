using TaskFetcher.Models.DTO;

namespace TaskFetcher.Services.Interfaces
{
    public interface ITaskService
    {
        Task<TimeTaskDTO> GetTask(Guid guid);
        Task<TimeTaskDTO> CreateTask();
    }
}
