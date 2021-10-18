using CORE.Prevail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Article : BaseModel
    {
        public string Caption { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImgPath { get; set; }

        // relations
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
