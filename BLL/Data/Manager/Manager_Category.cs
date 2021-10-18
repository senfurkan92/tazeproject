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
    public class Manager_Category : Manager_Generic<Category, IDal_Category>, IService_Category
    {
        public Manager_Category(IDal_Category dal) : base(dal)
        {

        }
    }
}
