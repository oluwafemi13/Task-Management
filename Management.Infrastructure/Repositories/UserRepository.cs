using Management.Application.Contracts;
using Management.Core.Entities;
using Management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var search = await _context.Users.
                                    Where(u => u.Email == email).
                                    FirstOrDefaultAsync();
            return search;
        }
    }
}
