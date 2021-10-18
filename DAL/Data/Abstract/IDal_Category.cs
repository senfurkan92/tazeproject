using CORE.Data.Dapper.Abstract;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Abstract
{
    public interface IDal_Category : IRepo_Dapper<Category>
    {
    }
}
