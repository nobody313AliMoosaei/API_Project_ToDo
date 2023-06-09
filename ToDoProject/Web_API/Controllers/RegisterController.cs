using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("Test/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private ToDo.Application.Services.User_Service.IUserService _UserService;
        public RegisterController(ToDo.Application.Services.User_Service.IUserService userservice)
        {
                _UserService= userservice;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] ToDo.Application.DTOs.RegisterUser_DTO Register_Data)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("ارسال اطلاعات به درستی انجام نشده است");
            }

           var Resualt_Add_User =  await _UserService.RegisterUser(Register_Data);

            if(Resualt_Add_User)
            {
                return Ok("کاربر با موفقیت ثبت نام شد");
            }
            return BadRequest("در ثبت نام کاربر مشکلی رخ داد");
        }
    }
}
