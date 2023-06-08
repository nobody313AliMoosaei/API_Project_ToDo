using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Services.User_Service
{
    public interface IUserService
    {
        Task<bool> IsExistUserAsync(string Value);
        Task<bool> IsAuthenticationUserAsync(string Value, string Password);
        Task<bool> IsExistUserAsync(int Id);
        Task<bool> RegisterUser(Application.DTOs.RegisterUser_DTO data);
        Task<ToDo.Domain.Entities.User> GetUserByUserNameOrEmailAsync(string Value);
    }
}
