using Microsoft.EntityFrameworkCore;
using TaskFetcher.Models.Entity;

namespace TaskFetcher.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TimeTask> Task { get; set; }

        public string DbPath { get; }

        public AppDbContext(DbContextOptions<AppDbContext>  options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "task.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
