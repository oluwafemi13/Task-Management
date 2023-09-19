using Management.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Services.Business_Logic
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepo;
        public TaskService(ITaskRepository taskRepo)
        {
            _taskRepo= taskRepo;
        }
        public async Task CreateTaskService(Management.Core.Entities.Task task)
        {
            var search = await _taskRepo.GetAsync(task.TaskId);
            if(search != null)
            {

            }
        }
    }
}
