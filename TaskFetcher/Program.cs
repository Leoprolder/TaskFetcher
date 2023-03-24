using Microsoft.EntityFrameworkCore;
using TaskFetcher.BackgroundWorker;
using TaskFetcher.Data;
using TaskFetcher.Models.MappingProfiles;
using TaskFetcher.Services;
using TaskFetcher.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddAutoMapper(typeof(TimeTaskMappingProfile));
builder.Services.AddDbContext<AppDbContext>(b => b.UseSqlite());
builder.Services.AddHostedService<TimerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
