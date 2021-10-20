using CORE.Data.Dapper.Abstract;
using CORE.Data.Dapper.Constant;
using CORE.Prevail.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CORE.Data.Dapper.Concrete
{
	/// <summary>
	/// Temel crud islemleri
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Repo_Dapper<T> : IRepo_Dapper<T>
		where T : BaseModel, new()
	{
		public SqlConnection Con { get; set; }

		private DynamicParameters Parameters { get; set; } = null;

		public Repo_Dapper(string connectionString)
		{
			Con = new SqlConnection(connectionString);
		}

        #region crud
        public ResultModel<IEnumerable<T>> Gets(T entity, string tableName, string orderBy = null, string sort = "desc", int offset = 0, int next = 30, SqlTransaction sqlTransaction = null)
		{
			// key belirlenmemis ise hata
			if (!HasKeyAttibute(entity))
			{
				return new ResultModel<IEnumerable<T>>(false, "Model key eksik", null);
			}

			// update ve insert'ten gelen get sorguları icin parametreyi sifirla
			this.Parameters = null;

			// donus tipi belirle
			ResultModel<IEnumerable<T>> resultModel = null;
			IEnumerable<T> data = null;

			// entity null gönderilse dahi hata verme
			if (entity == null) { entity = (T)Activator.CreateInstance(typeof(T)); }


			try
			{
				// orderby, next ve sort null olmasına karşı onlem
				orderBy = orderBy==null? GetIdColumnName(entity) : orderBy;
				next = next == 0 ? next = 30 : next;
				sort = string.IsNullOrEmpty(sort) ? sort = "desc" : sort;

				// select sorgusu olustur
				string query = $"select * from {tableName} {GetWhere_Query(entity, _enmWhereQueryType.All,true)} {GetPagenation_Query(entity,orderBy,sort,offset,next)}";

				// sorgula
				if (Parameters == null)
				{
					data = Con.Query<T>(query,null, sqlTransaction);
				}
				else
				{
					data = Con.Query<T>(query, Parameters, sqlTransaction);
				}

				if (data != null && data.Count() > 0)
				{
					resultModel = new ResultModel<IEnumerable<T>>(true, $"{data.Count()} adet veri getirildi", data);
				}
				else
				{
					resultModel = new ResultModel<IEnumerable<T>>(true, $"Herhangi bir veri getirilemedi", data);
				}
			}
			catch (Exception ex)
			{
				resultModel = new ResultModel<IEnumerable<T>>(false, ex.GetInnestException().Message, data);
			}
			return resultModel;
		}

		public ResultModel<T> Insert(T entity, string tableName, SqlTransaction sqlTransaction = null)
		{
			//key attribute oldugundan ve id null oldugundan emin ol
			if (!HasKeyAttibute(entity))
			{
				return new ResultModel<T>(false, "Model key eksik", null);
			}
			if (!KeyAttributeIsNull(entity))
			{
				return new ResultModel<T>(false, "Model key deger alamaz", null);
			}

			// inserts kısmında surekli bu method kullanilacagi icin sifirla
			this.Parameters = null;
			
			ResultModel<T> resultModel = null;
			T data = null;
			try
			{
				entity.IsActive = true;
				entity.IsDeleted = false;
				entity.InsertDate = DateTime.Now;
				entity.UpdateDate = DateTime.Now;
				string query = GetInsert_Query(entity, tableName);
				var id = 0;
				if (Parameters == null)
				{
					id = Con.ExecuteScalar<int>(query, null, sqlTransaction);					
				}
				else
				{
					id = Con.ExecuteScalar<int>(query, Parameters, sqlTransaction);
				}

				// insert entity i donmek icin id sini ata ve sorgula
				var keyProperty = Resolve(entity,_enmWhereQueryType.OnlyID);
				var keyPropertyName = string.Empty;
				foreach (var property in keyProperty)
				{
					keyPropertyName = property.Key;
					typeof(T).GetProperty(keyPropertyName).SetValue(entity, property.Value);
				}			
				data = Gets(new T { Id = id}, tableName, null, null, 0, 1, sqlTransaction).Data.ToList()?[0];

				if (data != null)
				{
					resultModel = new ResultModel<T>(true, $"Veri eklendi", data);
				}
				else
				{
					resultModel = new ResultModel<T>(true, $"Herhangi bir veri eklenemedi", data);
				}
			}
			catch (Exception ex)
			{
				resultModel = new ResultModel<T>(false, ex.GetInnestException().Message, data);
			}
			return resultModel;
		}

		public ResultModel<T> UpdateById(T entity, string tableName, SqlTransaction sqlTransaction = null)
		{
			//key attribute oldugundan ve id null olmadigindan emin ol
			if (!HasKeyAttibute(entity))
			{
				return new ResultModel<T>(false, "Model key eksik", null);
			}
			if(KeyAttributeIsNull(entity))
			{
				return new ResultModel<T>(false, "Model key null olamaz", null);
			}
			entity = IfIdNullMakeLesserZero(entity);

			// burada parametreler sifirlanmasa da olur
			this.Parameters = null;

			ResultModel<T> resultModel = null;
			T data = null;
			try
			{
				entity.UpdateDate = DateTime.Now;
				string query = GetUpdateById_Query(entity, tableName);
				if (Parameters == null)
				{
					var id = Con.ExecuteScalar<int>(query, null, sqlTransaction);
					// id icinde oldugu icin insert gibi id'yi vermeye gerek kalmaz
					data = Gets(entity, tableName,null,null,0,1,sqlTransaction).Data.ToList()?[0];
				}
				else
				{
					var id = Con.ExecuteScalar<int>(query, Parameters, sqlTransaction);
					// id icinde oldugu icin insert gibi id'yi vermeye gerek kalmaz
					data = Gets(new T { Id = id }, tableName, null, null, 0, 1, sqlTransaction).Data.ToList()?[0];
				}

				if (data != null)
				{
					resultModel = new ResultModel<T>(true, $"Veri güncellendi", data);
				}
				else
				{
					resultModel = new ResultModel<T>(true, $"Herhangi bir veri güncellenmedi", data);
				}
			}
			catch (Exception ex)
			{
				resultModel = new ResultModel<T>(false, ex.GetInnestException().Message, data);
			}
			return resultModel;
		}

		public ResultModel DeleteById(T entity, string tableName, SqlTransaction sqlTransaction = null)
		{
			//key attribute oldugundan ve id null olmadigindan emin ol
			if (!HasKeyAttibute(entity))
			{
				return new ResultModel<T>(false, "Model key eksik", null);
			}
			if (KeyAttributeIsNull(entity))
			{
				return new ResultModel<T>(false, "Model key null olamaz", null);
			}
			entity = IfIdNullMakeLesserZero(entity);

			// burada parametreler sifirlanmasa da olur
			this.Parameters = null;

			ResultModel resultModel = null;
			try
			{
				int data = 0;
				string query = GetDeleteById_Query(entity, tableName);
				if (Parameters == null)
				{
					data = Con.Execute(query, null, sqlTransaction);
				}
				else
				{
					data = Con.Execute(query, Parameters, sqlTransaction);
				}

				if (data > 0)
				{
					resultModel = new ResultModel(true, $"{data} adet veri silindi");
				}
				else
				{
					resultModel = new ResultModel(true, $"Herhangi bir veri silinemedi");
				}
			}
			catch (Exception ex)
			{
				resultModel = new ResultModel(false, ex.GetInnestException().Message);
			}
			return resultModel;
		}

		public SqlTransaction BeginTransaction() {
			Con.Open();
			var transaction = Con.BeginTransaction();
			return transaction;
		}

		public void CommitTransaction(SqlTransaction sqlTransaction)
		{
			sqlTransaction.Commit();
		}

		public void RollBackTransaction(SqlTransaction sqlTransaction)
		{
			sqlTransaction.Rollback();
		}
        #endregion


        #region constitute queries
        // get entity property and values
        private Dictionary<string, object> Resolve(T entity, _enmWhereQueryType queryType, bool nullable = false)
		{
			Dictionary<string, object> resolution = new Dictionary<string, object>();
			PropertyInfo[] properties = typeof(T).GetProperties();
			if (entity == null)
			{
				entity = (T)Activator.CreateInstance(typeof(T));
			}
			foreach (PropertyInfo property in properties)
			{
				var value = property.GetValue(entity);
				if (value != null || nullable)
				{
					if (queryType == _enmWhereQueryType.All)
					{
						resolution.Add(property.Name, value);
					}
					else
					{
						var keyAttribute = property.GetCustomAttributes(typeof(KeyAttribute), true);
						if (queryType == _enmWhereQueryType.OnlyID)
						{
							if (keyAttribute.Count() > 0)
							{
								resolution.Add(property.Name, value);
								break;
							}
						}
						else if (queryType == _enmWhereQueryType.WithoutID)
						{
							if (keyAttribute.Count() <= 0)
							{
								resolution.Add(property.Name, value);
							}
						}
					}
				}
			}
			return resolution;
		}

		// sorgular icin where query olusturma
		public string GetWhere_Query(T entity, _enmWhereQueryType queryType, bool allow1equal1 = false)
		{
			string sqlQuery = " where ";
			var resolution = Resolve(entity, queryType);
			if (Parameters == null) Parameters = new DynamicParameters();
			foreach (var item in resolution)
			{
				if (item.Value.ToString().Contains("||") && item.Value.GetType() == typeof(string) && allow1equal1)
				{ 
					string sqlQueryPiece = "(";
					string[] options = item.Value.ToString().Split("||");
					for (int i = 0; i < options.Length; i++)
					{
						sqlQueryPiece += $"{item.Key} = @{item.Key}W{i} or ";
						Parameters.Add($"@{item.Key}W{i}", options[i]);
					}
					sqlQueryPiece = sqlQueryPiece.TrimEnd();
					sqlQueryPiece = sqlQueryPiece.Substring(0, sqlQueryPiece.Length - 2);
					sqlQueryPiece += ") and ";
					sqlQuery += sqlQueryPiece;
				}
				else
				{
					sqlQuery += $"{item.Key} = @{item.Key}W and ";
					Parameters.Add($"@{item.Key}W", item.Value);
				}
			}

			// get sorgularinda eklenir
			if (allow1equal1)
			{
				sqlQuery += "1=1 ";
			}
			// update, delete durmunda son 'and' kaldirilir
			else
			{
				sqlQuery = sqlQuery.TrimEnd();
				sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 3);
			}
			return sqlQuery;
		}

		// sayfala sorgusu
		private string GetPagenation_Query(T entity, string orderBy, string sort, int offset, int next) 
		{
			if (Parameters == null) Parameters = new DynamicParameters();		
			if (orderBy == null || orderBy=="")
			{
				var properties = Resolve(entity, _enmWhereQueryType.OnlyID, true);
				foreach (var property in properties)
				{
					orderBy = property.Key;
				}
			}
			sort = sort == null ? "desc" : sort;
			if (next == -1)
			{
				return $"Order by {orderBy} {sort} offset {offset} rows";
			}
			else {
				return $"Order by {orderBy} {sort} offset {offset} rows fetch next {next} rows only";
			}		
		}

		// insert sorgusu icin query olusturma
		private string GetInsert_Query(T entity, string tableName)
		{
			var resolution = Resolve(entity, _enmWhereQueryType.WithoutID);
			string columns = string.Empty;
			string values = string.Empty;
			if (Parameters == null) Parameters = new DynamicParameters();
			foreach (var item in resolution)
			{
				columns += $"{item.Key},";
				values += $"@{item.Key}I,";
				Parameters.Add($"@{item.Key}I", item.Value);
			}
			string sqlQuery = $"insert into {tableName} ({columns.Substring(0, columns.Length - 1)}) output inserted.{GetIdColumnName(entity)} values ({values.Substring(0, values.Length - 1)})";
			return sqlQuery;
		}

		// update sorgusu icin query olusturma
		private string GetUpdateById_Query(T entity, string tableName)
		{
			entity = IfIdNullMakeLesserZero(entity);
			string sqlQuery = $"update {tableName} set ";
			var resolution = Resolve(entity, _enmWhereQueryType.WithoutID);
			if (Parameters == null) Parameters = new DynamicParameters();
			foreach (var item in resolution)
			{
				sqlQuery += $"{item.Key} = @{item.Key}U,";
				Parameters.Add($"@{item.Key}U", item.Value);
			}
			string whereQuery = GetWhere_Query(entity, _enmWhereQueryType.OnlyID);
			if (whereQuery == " where 1=1 ")
			{
				whereQuery = " where 1=2";
			}
			if (!whereQuery.Contains(GetIdColumnName(entity)))
			{
				whereQuery += " and 1=2 ";
			}
			sqlQuery = sqlQuery.Substring(0, sqlQuery.Length - 1);
			sqlQuery += $" output inserted.{GetIdColumnName(entity)} {whereQuery}";
			return sqlQuery;
		}

		// delete sorgusu icin query olusturma
		private string GetDeleteById_Query(T entity, string tableName)
		{
			entity = IfIdNullMakeLesserZero(entity);
			var sqlQuery = $"delete from {tableName} ";
			var whereQuery = GetWhere_Query(entity, _enmWhereQueryType.OnlyID);
			if (whereQuery == " where 1=1 " || whereQuery.Split('=').Length <= 2 || !whereQuery.Contains("and"))
			{
				whereQuery = " where 1=2";
			}
			if (!whereQuery.Contains(GetIdColumnName(entity)))
			{
				whereQuery += " and 1=2 ";
			}
			return sqlQuery += whereQuery;
		}

		// key property name alma
		private string GetIdColumnName(T entity)
		{
			var resolved = Resolve(entity, _enmWhereQueryType.OnlyID, true);
			foreach (var idColumn in resolved)
			{
				return idColumn.Key.ToString();
			}
			return null;
		}

		// key attribute iceren property yok ise hata don
		private bool HasKeyAttibute(T entitiy)
		{
			foreach (var prop in typeof(T).GetProperties())
			{
				foreach (var attribute in prop.GetCustomAttributes())
				{
					if (attribute.GetType() == typeof(KeyAttribute))
					{
						return true;
					}
				}
			}
			return false;
		}

		// key attribute update ve delete icin null olamaz
		// insert icin null olmali
		private bool KeyAttributeIsNull(T entitiy)
		{
			var keyProp = GetIdColumnName(entitiy);
			var value = typeof(T).GetProperty(keyProp).GetValue(entitiy);
			if (value == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		// Id update ve delete null kalmıs ise Id yi -1 yapma
		private T IfIdNullMakeLesserZero(T entity)
		{
			var keyPropName = GetIdColumnName(entity);
			var value = typeof(T).GetProperty(keyPropName).GetValue(entity);
			if (value == null)
			{
				typeof(T).GetProperty(keyPropName).SetValue(entity, -1);
				return entity;
			}
			else
			{
				return entity;
			}
		}
		#endregion
	}
}
