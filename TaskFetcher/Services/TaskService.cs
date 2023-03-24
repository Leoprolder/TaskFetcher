using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskFetcher.Data;
using TaskFetcher.Models.DTO;
using TaskFetcher.Models.Entity;
using TaskFetcher.Models.Enums;
using TaskFetcher.Services.Interfaces;

namespace TaskFetcher.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TimeTaskDTO> CreateTask()
        {
            var task = new TimeTask()
            {
                Guid = Guid.NewGuid(),
                Status = Status.Created,
                Created = DateTime.UtcNow
            };

            _context.Entry(task).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return _mapper.Map<TimeTaskDTO>(task);
        }

        public async Task<TimeTaskDTO> GetTask(Guid guid)
        {
            var task = await _context.Task.FindAsync(guid);
            return _mapper.Map<TimeTaskDTO>(task);
        }
    }
}
