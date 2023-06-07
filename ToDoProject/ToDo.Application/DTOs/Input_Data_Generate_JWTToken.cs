using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.DTOs
{
    public class Input_Data_Generate_JWTToken
    {
        public int User_Id { get; set; }
        public string? UserName { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();
    }
}
