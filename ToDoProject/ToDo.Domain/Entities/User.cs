using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDo.Domain.Entities
{
    public class User:IdentityUser<int>
    {
        public string? FullName { get; set; }

        // Annotation

        public List<Card>? Cards{ set; get; }
    }
}
