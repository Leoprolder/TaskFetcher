using TaskFetcher.Data;
using TaskFetcher.Models.Entity;
using TaskFetcher.Models.Enums;

namespace TaskFetcher.BackgroundWorker
{
    public sealed class TimerService : IHostedService, IAsyncDisposable
    {
        private readonly Task _completedTask = Task.CompletedTask;
        private readonly IServiceProvider _serviceProvider;

        private Timer? _timer;

        public TimerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWorkAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return _completedTask;
        }

        private void DoWorkAsync(object? state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var tasks = context.Task.ToList().Where(x => 
                    x.Status == Status.Created || IsToFinish(x));

                foreach (var task in tasks)
                {
                    task.Status = IsToFinish(task) ? Status.Finished : Status.Running;
                }

                context.SaveChanges();
            }
        }

        private bool IsToFinish(TimeTask task)
        {
            return (DateTime.UtcNow - task.Created).TotalMinutes > 2;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return _completedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_timer is IAsyncDisposable timer)
            {
                await timer.DisposeAsync();
            }

            _timer = null;
        }
    }
}
