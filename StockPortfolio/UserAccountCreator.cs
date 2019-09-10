using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockPortfolio
{
    public class UserAccountCreator
    {
        public void CreateUserAccount(ProgramContext programContext)
        {
            this.CreateUserName(programContext);
            this.CreateUserPassword(programContext);
            this.AddUserToDataBase(programContext);
            programContext.UserInterface.StartUpOptions(programContext);
        }
        public void CreateUserName(ProgramContext programContext)
        {
            Console.WriteLine("Please create a Username");
            var tempUserName = Console.ReadLine();

            if (string.IsNullOrEmpty(tempUserName))
            {
                // invalid entry
                Console.WriteLine("Invalid entry please try again");
                this.CreateUserName(programContext);
            }
            else
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
                {
                    var ownedUserNames = connection.Query<User>(
                        $@"select username
                        from AccountLoginInfo
                        where username = '{tempUserName}'").ToList();
                    if (ownedUserNames.Count != 0)
                    {
                        Console.WriteLine("Username is in use please pick another");
                        this.CreateUserName(programContext);
                    }
                    else
                    {
                        programContext.User.UserName = tempUserName;
                    }
                }
            }
        }

        public void CreateUserPassword(ProgramContext programContext)
        {
            Console.WriteLine("Please create a password");
            var tempUserPassword = Console.ReadLine();

            if (string.IsNullOrEmpty(tempUserPassword))
            {
                // invalid entry
                Console.WriteLine("Invalid entry please try again");
                this.CreateUserPassword(programContext);
            }
            else
            {
                programContext.User.Password = tempUserPassword;
            }
        }

        public void AddUserToDataBase(ProgramContext programContext)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
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
