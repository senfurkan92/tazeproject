using AutoMapper;
using Domain;
using DTO.ArticleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models.Profiles
{
    public class ArticleProfile : Profile 
    {
        public ArticleProfile()
        {
            CreateMap<ArticleInsertDto, Article>();
            CreateMap<Article, ArticlePresentDto>();
        }
    }
}
