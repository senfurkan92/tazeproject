using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ArticleDtos
{
    public class ArticlePresentDto
    {
        public int Id { get; set; }
        public string Caption { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImgPath { get; set; }

        public string CategoryName { get; set; }
    }
}
