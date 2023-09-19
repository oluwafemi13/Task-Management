using Management.Infrastructure.Services.Worker.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services.Background_Services
{
    public class AssignedNewTaskService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AssignedNewTaskService> _logger;
        public AssignedNewTaskService(ILogger<AssignedNewTaskService> logger, IServiceProvider serviceProvider)
        {

            _logger = logger;

            _serviceProvider = serviceProvider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await DoWork(stoppingToken);

        }



        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(AssignedNewTaskService)} is working.");

            using var scope = _serviceProvider.CreateScope();
            var workService = scope.ServiceProvider.GetRequiredService<IWorker>();

            await workService.DoWorkAsync(stoppingToken);



        }

    }
}
