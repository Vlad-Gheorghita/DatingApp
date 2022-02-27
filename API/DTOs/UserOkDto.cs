using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.DTOs
{
    public class UserOkDto
    {
        public UserDto User { get; set; }
        public bool IsOk { get; set; } = false;
        public string Message { get; set; }
        public  IEnumerable<IdentityError> Error { get; set; }
    }
}