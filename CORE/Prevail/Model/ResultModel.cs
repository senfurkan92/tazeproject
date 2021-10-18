using CORE.Prevail.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Prevail.Model
{
	public class ResultModel
	{
		public bool Success { get; set; }

		public string Description { get; set; }

		public ResultModel()
		{

		}

		public ResultModel(bool success, string description)
		{
			Success = success;
			Description = description;
		}
	}

	public class ResultModel<TData> : ResultModel
	{
		public TData Data { get; init; }

		public ResultModel()
		{
				
		}

		public ResultModel(bool success, string description, TData data) : base (success,description)
		{
			Data = data;
		}
	}
}
