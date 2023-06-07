using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities
{
    public class Role:IdentityRole<int>
    {
        [Required(ErrorMessage ="وارد کردن نام فارسی نقش مدنظر اجباری است")]
        public string? PersianName { get; set; }
    }
}
