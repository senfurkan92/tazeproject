using CORE.Data.Dapper.Abstract;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Abstract
{
    /// <summary>
    /// Article sinifi icin repository pattern
    /// </summary>
    public interface IDal_Article : IRepo_Dapper<Article>
    {
    }
}
