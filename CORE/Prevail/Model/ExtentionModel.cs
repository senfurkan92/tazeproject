using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Prevail.Model
{
	public static class ExtentionModel
	{
		/// <summary>
		/// olusan hatalarda en dip hatanin bulunmasi
		/// </summary>
		/// <param name="ex"></param>
		/// <returns></returns>
		public static Exception GetInnestException(this Exception ex)
		{
			if (ex.InnerException == null)
			{
				return ex;
			}
			else
			{
				return ex.InnerException.GetInnestException();
			}
		}
	}
}
