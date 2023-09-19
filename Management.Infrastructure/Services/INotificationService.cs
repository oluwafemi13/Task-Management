using Management.Application.Models;
using Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAll(RequestParameters parameter);
        Task<Reponse> Update(Notification notification, int Id);
        Task<Reponse> DeleteNotification(int Id);
        Task<Reponse> CreateNotification(Notification notification);
        Task<Reponse> GetNotificationById(int UserId);
        Task<Reponse> MarkAsRead(int Id);
    }
}
