using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.DTOs
{
    public class Create_Card_DTO
    {
        [Required(ErrorMessage = "وارد کردن عنوان الزامی است")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public List<int>? User_IDs { get; set; } = new List<int>();
    }
}
