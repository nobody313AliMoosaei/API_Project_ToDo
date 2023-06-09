using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.DTOs
{
    public class OutPut_Card
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? UserNames { get; set; } = new List<string>();
    }
}
