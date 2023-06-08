using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.DTOs
{
    public class LoginUser_DTO
    {
        [Required(ErrorMessage ="وارد کردن نام کاربری یا ایمیل اجباری است")]
        public string? UserName { get; set; }

        [Required(ErrorMessage ="وارد کردن رمز عبور اجباری است")]
        public string? Password { get; set; }
        public bool? IsRemmeber { get; set; } = false;
    }
}
