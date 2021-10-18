using CORE.Prevail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        
        // relations
        public virtual ICollection<Article> Articles { get; set; }

        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
