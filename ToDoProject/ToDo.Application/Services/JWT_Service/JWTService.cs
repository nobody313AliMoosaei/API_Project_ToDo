using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;

namespace ToDo.Application.Services.JWT_Service
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _Configuration;

        public JWTService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public async Task<string> GenrateTokenAysnc(Input_Data_Generate_JWTToken Data)
        {
            try
            {
                if (Data.User_Id == 0 || string.IsNullOrWhiteSpace(Data.UserName))
                {
                    return string.Empty;
                }

                var Claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, Data.User_Id.ToString()),
                    new Claim(ClaimTypes.Name, Data.UserName)
                };
                if (Data.Roles!=null || Data.Roles.Count > 0)
                {
                    foreach (var item in Data.Roles)
                    {
                        Claims.Add(new Claim(ClaimTypes.Role,item));
                    }
                }

                string Key = _Configuration["JWTConfiguration:key"];
                var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
                var Credential = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);

                var Token =
                new JwtSecurityToken(
                    issuer: _Configuration["JWTConfiguration:issuer"],
                    audience: _Configuration["JWTConfiguration:audience"],
                    expires: DateTime.Now.AddDays(10),
                    claims: Claims,
                    signingCredentials: Credential
                    );

                string jwt_Token = new JwtSecurityTokenHandler().WriteToken(Token);
                return jwt_Token;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
