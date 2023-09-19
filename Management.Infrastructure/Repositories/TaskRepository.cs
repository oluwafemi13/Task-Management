using Azure;
using Management.Application.Contracts;
using Management.Core.Entities;
using Management.Core.Enums;
using Management.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Management.Application.Models;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Management.Application.DTO;
using System.Reflection;
using System.Globalization;
using Management.Infrastructure.Services;

namespace Management.Infrastructure.Repositories
{
    public class TaskRepository : BaseRepository<Management.Core.Entities.Task> , ITaskRepository
    {
        // private readonly DatabaseContext _context;
        private readonly IProjectService _projectService;
        public TaskRepository(DatabaseContext context, IProjectService projectService): base(context) 
        {
            _projectService = projectService;  
        }

        public async Task<Management.Core.Entities.Task> CreateAsync(TaskDto task)
        {
            try
            {
                var search = await _context.Tasks.FindAsync(task.TaskId);
                var checkProject = await _context.Projects.Where(x => x.ProjectId == task.Project.ProjectId).FirstOrDefaultAsync();

                
                var pri = task.Priority;
                switch (pri)
                {
                    case Priority.high:
                        task.Priority = Priority.high;
                        break;

                    case Priority.medium:
                        task.Priority = Priority.medium;
                        break;

                    case Priority.low:
                        task.Priority = Priority.low;
                        break;

                    default: throw new ArgumentException();

                };
                var project = new Project();
                if (checkProject == null)
                {
                     project = new Project
                    {
                        Description = task.Project.Description,
                        Name = task.Project.Name

                    };
                }
                else
                {
                    project = new Project
                    {
                        ProjectId = checkProject.ProjectId
                    };
                }
                var user = new User
                {
                    Name = task.User.Name,
                    Email = task.User.Email,

                };
                var tasks = new Management.Core.Entities.Task
                {
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Priority = pri,
                    Status = task.Status,
                    Title = task.Title,
                    Project = project,
                    DateCreated = DateTime.Now,
                    //Project.ProjectId = checkProject.ProjectId,
                    User = user

                };
                if (search == null)
                {
                    await _context.Tasks.AddAsync(tasks);
                    await _context.SaveChangesAsync();
                    return tasks;
                }

                return tasks;

            }
            catch (Exception)
            {
                
                throw;
            }
            
        }
        public async Task<Reponse> UpdateTask(int Id, TaskDto task)
        {
            try
            {
                var search = await _context.Tasks.Where(x=> x.TaskId== Id).FirstOrDefaultAsync();
                if (search == null)
                {
                    return new Reponse
                    {
                        Data = null,
                        IsSuccess = false,
                        ResponseCode = 404,
                        ReasonPhrase = "Data Not Found"
                    };
                }
                search.Title = task.Title;
                search.Status = task.Status;
                search.Description = task.Description;
                search.DueDate = task.DueDate;
                search.Priority = task.Priority;
                search.TaskId = Id;
                var update = _context.Tasks.Update(search);
                await _context.SaveChangesAsync();
                return new Reponse
                {
                    Data = null,
                    IsSuccess = true,
                    ReasonPhrase = "Successful",
                    ResponseCode = 200
                };
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public async Task<bool> DeleteTask(int Id) 
        {
            try
            {
                var search = await _context.Tasks.FindAsync(Id);
                if(search == null)
                {
                    return false;
                } 
                _context.Tasks.Remove(search);  
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Management.Core.Entities.Task>> GetAllTimedTask()
        {
            var currentTime = DateTime.Now;
            var last24Hours = currentTime.AddHours(-24);

            var search = await _context.Tasks.Where(x => x.DateCreated >= last24Hours).ToListAsync();
            return search;
        }

        public async Task<IEnumerable<Management.Core.Entities.Task>> GetAllTask(int pageIndex, int pageSize)
        {
            var search = await _context.Tasks.
                                        ToPagedListAsync<Management.Core.Entities.Task>(pageIndex, pageSize);
            return search;
        }

        public async Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByUser(int userId, int pageIndex, int pageSize)
        {
            var search = await _context.Tasks.
                                            Where(x=> x.User.UserId == userId).
                                            ToPagedListAsync(pageIndex, pageSize);

            return search;
        }

        public async Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByTitle(string Title, int userId)
        {
            //var title = Title.Substring(3);
            var search = await _context.Tasks.
                                        Where(x => x.Title == Title).
                                        ToListAsync();
                                        
            return search;
        }

        public async Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByStatus(string status)
        {
            var stat = status.ToLower().Replace(" ", "").Trim();
            var stats = Status.pending;
            var search = new List<Management.Core.Entities.Task>();
            
            switch (stat)
            {
                case "pending":
                    search = await _context.Tasks.Where(x => x.Status == Status.pending).ToListAsync();
                    break;
                case "inprogress":
                    search = await _context.Tasks.Where(x => x.Status == Status.in_progress).ToListAsync();
                    break;
                case "completed":
                    search = await _context.Tasks.Where(x => x.Status == Status.completed).ToListAsync();
                    break;
                default:
                    throw new ArgumentException();

            }
            
            return search;

        }

        public async Task<IEnumerable<Management.Core.Entities.Task>> GetTaskForWeek()
        {
            var currentWeek = DateTime.Now;
           var task = new List<Management.Core.Entities.Task>();    

            var search = await _context.Tasks.Where(x=> x.DueDate.Year == currentWeek.Year)
                                        .Where(m=>m.DueDate.Month == currentWeek.Month)
                                        .ToListAsync();

            CultureInfo culture = CultureInfo.CurrentCulture;
            Calendar calendar = culture.Calendar;
            var checkCurrentWeek = calendar.GetWeekOfYear(currentWeek, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
            foreach (var date in search)
            {
                var check = calendar.GetWeekOfYear(date.DueDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
                if(check == checkCurrentWeek)
                {
                    task.Add(date);
                }

            }
            return task;

            
        }

        public async Task<IEnumerable<Management.Core.Entities.Task>> GetTaskByRead()
        {
            var readTask = await _context.Tasks.Where(x=> x.Status == Status.completed).ToListAsync();
            return readTask;
        }

        public async Task<Management.Core.Entities.Task> AssignTaskToProject(int taskId, int newProjectId)
        {
            var searchTask =await _context.Tasks.Where(x => x.TaskId == taskId).FirstOrDefaultAsync();
            if(searchTask != null)
            { 
                var result = await _projectService.GetProjectById(newProjectId);
                if(result.IsSuccess == true)
                {
                    searchTask.ProjectId = newProjectId;
                    var update = _context.Tasks.Update(searchTask);
                    await _context.SaveChangesAsync();

                }
            }

            return searchTask;


        }

    }
}
