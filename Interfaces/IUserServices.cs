using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos;

namespace OnlineCoursesUI.Interfaces
{
    public interface IUserServices
    {
        Task<HttpResponseMessage> LoginUser (LoginDto loginDto);
        Task<bool> RegisterUser (RegisterDto registerDto);
    }
}