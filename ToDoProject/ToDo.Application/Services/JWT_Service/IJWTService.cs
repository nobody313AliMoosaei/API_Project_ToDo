using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.Services.JWT_Service
{
    public interface IJWTService
    {
        Task<string> GenrateTokenAysnc(Input_Data_Generate_JWTToken Data);
    }
}
