using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.DTOs
{
    public class RegisterUser_DTO
    {
        [Required(ErrorMessage ="وارد کردن نام و نام خانوادگی اجباری است")]
        public string? FullName { get; set; }

        [Required(ErrorMessage ="وارد کردن نام کاربری اجباری است")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "وارد کردن شماره تلفن اجباری است")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "وارد کردن رایانامه اجباری است")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "وارد کردن رمز عبور اجباری است")]
        [DataType(DataType.Password)]
        public string? Passowrd { get; set; }
    }
}
