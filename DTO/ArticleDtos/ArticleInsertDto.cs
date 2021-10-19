using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ArticleDtos
{
    public class ArticleInsertDto
    {
        public string Caption { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string ImgPathName { get {  return this.ImgFile.FileName.Trim().Replace(" ","").ToLower(); } }

        public IFormFile ImgFile{ get; set; }

        public int CategoryId { get; set; }
    }
}
