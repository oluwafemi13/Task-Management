using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Application.DTO;
using Management.Application.Models;
using Management.Core.Entities;
using Management.Core.Enums;

namespace Management.Application.Contracts
{
    public interface ITaskRepository: IBaseRepository<Management.Core.Entities.Task>
    {
        // Task<Management.Core.Entities.Task> GetAsync(int Id);
        Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByRead();
        Task<Management.Core.Entities.Task> CreateAsync(TaskDto task);
        Task<Reponse> UpdateTask(int Id, TaskDto task);
        Task<IEnumerable<Management.Core.Entities.Task>> GetAllTimedTask();
        Task<bool> DeleteTask(int Id);
        Task<IEnumerable<Management.Core.Entities.Task>> GetAllTask(int pageIndex, int pageSize);
        Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByUser(int userId, int pageIndex, int pageSize);
        Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByTitle(string Title, int userId);
        Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByStatus(string status);
        Task<IEnumerable<Management.Core.Entities.Task>> GetTaskForWeek();
        Task<Management.Core.Entities.Task> AssignTaskToProject(int taskId, int newProjectId);

    }
}
