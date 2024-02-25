using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Task_1.DAL.Interfaces;
using Task_1.Domain.Entity;

namespace Task_1.Test;

public class TaskServiceTest
{
    private WebApplicationFactory<Program> _factory;
    
    [Fact]
    public async void CreateAsync_Task_Created()
    {
        //Avverage
        const int taskId = 1;

        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {
            builder.ConfigureServices(services => {
                var taskRepository = services
                    .SingleOrDefault(x => x.ServiceType == typeof(ITaskRepository));
                services.Remove(taskRepository);
                var mockTaskRepository = new Mock<ITaskRepository>();

                mockTaskRepository
                    .Setup(x => x.AddAsync(It.IsAny<Domain.Entity.Task>()))
                    .ReturnsAsync((Domain.Entity.Task task) => {
                        task.DatePublish = DateTime.Now;
                        task.Id = taskId;
                        return task;
                    });

                mockTaskRepository
                    .Setup(x => x.SaveChangesAsync())
                    .ReturnsAsync(true);

                services.AddScoped<ITaskRepository>(x => mockTaskRepository.Object);
            });
        });

        var task = new Domain.Entity.Task() 
        {
            Name = "Test",
            Text = "TestText"
        };

        HttpClient client = _factory.CreateClient();

        HttpContent httpContent = new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json");

        //Act
        var response = client.PostAsync("/api/Tasks", httpContent);

        Domain.Entity.Task result = await response.Result.Content.ReadFromJsonAsync<Domain.Entity.Task>();

        //Assert
        Assert.Equal(StatusCodes.Status201Created, (int)response.Result.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(taskId, result.Id);

    }
}