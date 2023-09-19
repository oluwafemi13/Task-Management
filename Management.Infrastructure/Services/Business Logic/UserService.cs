using Management.Application.Contracts;
using Management.Application.Models;
using Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Management.Infrastructure.Services.Business_Logic
{
    public class UserService : IUserservice
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Reponse> CreateUser(User user)
        {
            try
            {
                //var search = _repo.GetByEmailAsync(user.Email);
                
                    await _repo.CreateAsync(user);
                    return new Reponse
                    {
                        Data = JsonSerializer.Serialize(user),
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

        public async Task<Reponse> DeleteUser(int Id)
        {
            var search = await _repo.GetAsync(Id);
            if (search != null)
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

        public async Task<Reponse> Update(User user, int Id)
        {
            var search = await _repo.GetAsync(Id);
            if (search == null)
            {
                return new Reponse
                {
                    Data = null,
                    IsSuccess = false,
                    ReasonPhrase = "Data Not Found",
                    ResponseCode = 404
                };
            }
            search.Email = user.Email;
            //search.UserId = user.UserId;
            search.Name = user.Name;
            

            var result = await _repo.UpdateAsync(search, Id);
            if (result == null)
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

        public Task<IEnumerable<User>> GetAll(RequestParameters parameter)
        {
            var search = _repo.GetAllPagedAsync(parameter);
            return search;
        }
    }
}
