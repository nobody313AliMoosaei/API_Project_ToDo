using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Services.User_Service;
namespace Web_API.Controllers
{
    [Route("Test/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ToDo.Application.Services.User_Service.IUserService _UserService;
        private ToDo.Application.Services.JWT_Service.IJWTService _JWTService;

        public LoginController(IUserService userService, ToDo.Application.Services.JWT_Service.IJWTService jWTService)
        {
            _UserService = userService;
            _JWTService = jWTService;
        }


        /// <summary>
        /// ورودی کاربر در این اکشن اتفاق می افتد
        /// یک رشته استرینگ که شامل اطلاعات مورد نیاز برای سایت است
        /// به فرانت برگشت داده می شود
        /// رشته حاوی : شناسه کاربری و نام کاربری است
        /// </summary>
        /// <param name="Login_Data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] ToDo.Application.DTOs.LoginUser_DTO Login_Data)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            var Resualt = await _UserService.IsAuthenticationUserAsync(Login_Data.UserName, Login_Data.Password);
            if(!Resualt)
            {
                return BadRequest("چنین کاربری وجود ندارد");
            }
            var user = await _UserService.GetUserByUserNameOrEmailAsync(Login_Data.UserName);
            if (user != null)
            {
                string Token = await _JWTService.GenrateTokenAysnc(new ToDo.Application.DTOs.Input_Data_Generate_JWTToken
                {
                    User_Id = user.Id,
                    UserName = user.UserName,
                });
                return Ok(new ToDo.Application.DTOs.OutPut_Login_User
                {
                    Token= Token,
                    UserName = user.UserName
                });
            }
            return BadRequest("کاربری یافت نشد");
        }
    }
}
