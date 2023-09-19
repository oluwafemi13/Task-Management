using Management.Application.Contracts;
using Management.Core.Entities;
using Management.Infrastructure.Services.Worker.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services.Worker
{
    public class UserTaskCompleted : IWorker
    {
        private readonly ILogger<UserTaskCompleted> _logger;
        private readonly ITaskRepository _taskRepo;
        private readonly IServiceProvider _serviceProvider;

        public UserTaskCompleted(ILogger<UserTaskCompleted> logger, ITaskRepository taskRepo, IServiceProvider serviceProvider)
        {
            _logger = logger;
            //_complete= complete;
            _taskRepo = taskRepo;
            _serviceProvider = serviceProvider;
        }
        public async System.Threading.Tasks.Task DoWorkAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {

                var completedTask = await _taskRepo.GetTaskByRead();
                if (completedTask != null)
                {
                    foreach (var task in completedTask)
                    {
                        Console.WriteLine($"Task{task.Title} with user {task.UserId} Has been completed ");
                        _logger.LogInformation($"Task{task.Title} with user {task.UserId} Has been completed");
                    }

                    await System.Threading.Tasks.Task.Delay(6000);
                }

            }
            await System.Threading.Tasks.Task.CompletedTask;
        }
        }
    }

