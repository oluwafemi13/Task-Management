﻿using Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.Contracts
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
