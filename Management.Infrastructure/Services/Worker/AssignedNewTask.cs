using Management.Application.Contracts;
using Management.Infrastructure.Services.Worker.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services.Worker
{
    public class AssignedNewTask : IWorker
    {
        private readonly ITaskRepository _taskRepo;
        private readonly ILogger<AssignedNewTask> _logger;
        public AssignedNewTask(ITaskRepository taskRepo, ILogger<AssignedNewTask> logger)
        {
            _taskRepo= taskRepo;
            _logger= logger;
        }
        public async Task DoWorkAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {

                var tasks = await _taskRepo.GetAllTimedTask();
                foreach (var task in tasks)
                {
                    DateTime now = DateTime.Now;
                    DateTime previous12Hours = now.AddHours(-12);
                    if (task.DateCreated <= now || previous12Hours <=task.DateCreated )
                    {
                        Console.WriteLine($"Task{task.Title} has been created within the last 12 Hours ");
                        _logger.LogInformation($"Task {task.Title} has been created within the last 12 Hours");
                    }
                    await Task.Delay(5000);
                }
            }
            await Task.CompletedTask;
        }
    }
}
