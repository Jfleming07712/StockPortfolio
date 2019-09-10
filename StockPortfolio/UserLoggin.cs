using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockPortfolio
{
    public class UserLoggin
    {
        public void LogginUser(User user)
        {
            Console.WriteLine("Please enter your Username");
            var givenUserName = Console.ReadLine();


            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                var ownedUserNames = connection.Query<User>(
                    $@"select username
                        from AccountLoginInfo
                        where username = '{givenUserName}'").ToList();
                foreach (User user in RegisteredUsers)
            }
        }
    }
}
