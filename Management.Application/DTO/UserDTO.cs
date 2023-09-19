using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
