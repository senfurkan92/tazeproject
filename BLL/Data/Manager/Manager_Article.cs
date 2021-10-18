using BLL.Data.Service;
using DAL.Data.Abstract;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data.Manager
{
    public class Manager_Article : Manager_Generic<Article,IDal_Article>, IService_Article
    {
        public Manager_Article(IDal_Article dal) : base(dal)
        {

        }
    }
}
