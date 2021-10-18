using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        // relations
        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
