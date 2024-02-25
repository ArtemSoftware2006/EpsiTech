using Microsoft.EntityFrameworkCore;
using Task_1.DAL;
using Task_1.DAL.Implementations;
using Task_1.DAL.Interfaces;
using Task_1.Middlewares;
using Task_1.Services.Implementations;
using Task_1.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseInMemoryDatabase("Task_1");
});
builder.Services.AddLogging();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
