using CORE.Data.Dapper.Concrete;
using DAL.Data.Abstract;
using DAL.Helper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Concrete
{
    public class Dal_Category : Repo_Dapper<Category>, IDal_Category
    {
        public Dal_Category() : base(CstrHelper.GetConnectionString())
        {

        }
    }
}
