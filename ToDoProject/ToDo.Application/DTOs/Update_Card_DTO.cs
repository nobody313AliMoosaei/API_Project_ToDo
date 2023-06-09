using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.DTOs
{
    public class Update_Card_DTO
    {
        [Required]
        public int Card_Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public List<int>? User_Ids { get; set; }
    }
}
