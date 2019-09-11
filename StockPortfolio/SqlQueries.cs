using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StockPortfolio
{
    public class SqlQueries
    {
        public void AddRowToTable(ProgramContext programContext)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                var column1

                connection.Query(
$@"declare @username nvarchar(max)
declare @password nvarchar(max)
set @username = '{programContext.User.UserName}'
set @password = '{programContext.User.Password}'
insert into AccountLoginInfo (Username, Password)
values (@username, @password)");
            }
        }
    }
}
