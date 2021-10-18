using BLL.Data.Service;
using CORE.Data.Dapper.Abstract;
using CORE.Prevail.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data.Manager
{
    public class Manager_Generic<T, TIDal> : IService_Generic<T>
        where T : BaseModel
        where TIDal : IRepo_Dapper<T>
    {
        protected readonly TIDal dal;

        public Manager_Generic(TIDal dal)
        {
            this.dal = dal;
        }

        public SqlTransaction BeginTransaction()
        {
            return dal.BeginTransaction();
        }

        public void CommitTransaction(SqlTransaction sqlTransaction)
        {
            dal.CommitTransaction(sqlTransaction);
        }

        public ResultModel DeleteById(T entity, string tableName, SqlTransaction sqlTransaction = null)
        {
            return dal.DeleteById(entity, tableName, sqlTransaction);
        }

        public ResultModel<IEnumerable<T>> Gets(T entity, string tableName, string orderBy, string sort, int offset, int next, SqlTransaction sqlTransaction = null)
        {
            return dal.Gets(entity, tableName, orderBy, sort, offset, next, sqlTransaction);
        }

        public ResultModel<T> Insert(T entity, string tableName, SqlTransaction sqlTransaction = null)
        {
            return dal.Insert(entity, tableName, sqlTransaction);
        }

        public void RollBackTransaction(SqlTransaction sqlTransaction)
        {
            dal.RollBackTransaction(sqlTransaction);
        }

        public ResultModel<T> UpdateById(T entity, string tableName, SqlTransaction sqlTransaction = null)
        {
            return dal.UpdateById(entity, tableName, sqlTransaction);
        }
    }
}
