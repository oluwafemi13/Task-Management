using Management.Application.Contracts;
using Management.Application.Models;
using Management.Core.Entities;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;

        public NotificationService(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task<Reponse> CreateNotification(Notification notification)
        {
            try
            {
                await _repo.CreateAsync(notification);
                return new Reponse
                {
                    Data = JsonSerializer.Serialize(notification),
                    IsSuccess = true,
                    ReasonPhrase = "Created",
                    ResponseCode = 201
                };
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<Reponse> DeleteNotification(int Id)
        {
            var search = await _repo.GetAsync(Id);
            if(search != null)
            {
                await _repo.DeleteAsync(search);
                return new Reponse
                {
                    Data = search,
                    IsSuccess = true,
                    ReasonPhrase = "Successful",
                    ResponseCode = 200
                };
            }
            return new Reponse
            {
                Data = null,
                IsSuccess = false,
                ReasonPhrase = "Not Found",
                ResponseCode = 404
            };
        }
        

        public Task<IEnumerable<Notification>> GetAll(RequestParameters parameter)
        {
           
            var search = _repo.GetAllPagedAsync(parameter);
            return search;
        }

        public async Task<Reponse> GetNotificationById(int UserId)
        {
            try
            {
                var search =await _repo.GetAsync(UserId);
                if(search == null)
                {
                    return new Reponse
                    {
                        Data = null,
                        IsSuccess = false,
                        ReasonPhrase = "Data Not Found",
                        ResponseCode = 404
                    };
                }
                return new Reponse
                {
                    Data = search,
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

        public async Task<Reponse> MarkAsRead(int Id)
        {
            try
            {
                var search = await _repo.GetAsync(Id);
                if(search != null)
                {
                    search.Read = true;
                    await _repo.UpdateAsync(search, Id);
                    return new Reponse
                    {
                        Data = search,
                        IsSuccess = true,
                        ReasonPhrase = "Updated Successfully",
                        ResponseCode = 200
                    };

                   
                }

                return new Reponse
                {
                    Data = search,
                    IsSuccess = false,
                    ReasonPhrase = "failed to Update",
                    ResponseCode = 400
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Reponse> Update(Notification notification, int Id)
        {
            var search = await _repo.GetAsync(Id);
            if(search == null)
            {
                return new Reponse
                {
                    Data = null,
                    IsSuccess = false,
                    ReasonPhrase = "Data Not Found",
                    ResponseCode = 404
                };
            }
            search.NotificationId = Id;
            search.Read = notification.Read;
            search.TimeStamp = notification.TimeStamp;
            search.Message = notification.Message;
            search.Type = notification.Type;
            
           var result =  await _repo.UpdateAsync(search, Id);
            if(result == null)
            {
                return new Reponse
                {
                    Data = null,
                    IsSuccess = false,
                    ReasonPhrase = "Failed to Update",
                    ResponseCode = 500
                };
            }
            return new Reponse
            {
                Data = JsonSerializer.Serialize(result),
                IsSuccess = true,
                ReasonPhrase = "Successful",
                ResponseCode = 200
            };
        }
    }
}
