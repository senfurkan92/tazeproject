using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Data.Dapper.Constant;
using CORE.Prevail.Model;
using Dapper;
using System.Transactions;

namespace CORE.Data.Dapper.Abstract
{
	public interface IRepo_Dapper<T>
		where T : BaseModel
	{
		// gelen entity'nin null olmayan değerleri için entity listesi dönecek
		ResultModel<IEnumerable<T>> Gets (T entity, string tableName, string orderBy, string sort, int offset, int next, SqlTransaction sqlTransaction = null);

		// gelen entity'yi insert edip id'li şekilde dönecek
		ResultModel<T> Insert (T entity, string tableName, SqlTransaction sqlTransaction = null);

		// gelen entity'yi update edip güncel halini dönecek
		ResultModel<T> UpdateById(T entity, string tableName, SqlTransaction sqlTransaction = null);

		// gelen entity'yi delete edip sonuc dönecek
		ResultModel DeleteById(T entity, string tableName, SqlTransaction sqlTransaction = null);

		//transaction
		SqlTransaction BeginTransaction();

		void CommitTransaction(SqlTransaction sqlTransaction);

		void RollBackTransaction(SqlTransaction sqlTransaction);
	}	
}
