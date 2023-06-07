using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Domain.Entities
{
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="وارد کردن عنوان کارت ضروری است")]
        public string? Title { get; set; }

        [Required(ErrorMessage ="توضیحات اجباری است")]
        public string? Description { get; set; }

        public ToDo.Domain.Enums.CardStatus Status { get; set; }

        // هر کاربر می تواند چند تا کارت داشته باشد
        // هر کارت می تواند متعلق به چند یوزر باشد

        public List<User>? Users { get; set; }
    }
}
