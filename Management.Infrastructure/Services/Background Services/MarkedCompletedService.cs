using Management.Application.Contracts;
using Management.Infrastructure.Services.Worker;
using Management.Infrastructure.Services.Worker.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services.Background_Services
{
    public class MarkedCompletedService : BackgroundService
    {
        //private readonly IMarkedComplete _marked;
        //private readonly ITaskRepository _taskRepo;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MarkedCompletedService> _logger;
        public MarkedCompletedService(ILogger<MarkedCompletedService> logger, /*ITaskRepository taskRepo*/ IServiceProvider serviceProvider)
        {
            /*_marked= marked;
            _serviceProvider= serviceProvider;*/
            _logger= logger;
           // _taskRepo= taskRepo;
           _serviceProvider= serviceProvider;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            await  DoWork(stoppingToken);

        }

    

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                $"{nameof(MarkedCompletedService)} is working.");

            using var scope = _serviceProvider.CreateScope();
            var workService = scope.ServiceProvider.GetRequiredService<IWorker>();
                
                await workService.DoWorkAsync(stoppingToken);

               
            
        }

      
    }
}
