using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{

    /// <summary>
    /// Dapper ve EntityFramework icin connectionString
    /// </summary>
    public static class CstrHelper
    {
        private static string connectionString = "Data Source=.; Initial Catalog=TazeProjectDb; Integrated Security=true;";

        public static string GetConnectionString() => connectionString;
    }
}
