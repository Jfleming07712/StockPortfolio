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
        public void LogginUser(ProgramContext programContext)
        {
            Console.WriteLine("Please enter your Username");
            var givenUserName = Console.ReadLine();


            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                var registeredUser = connection.QuerySingle<User>(
                    $@"select *
                        from AccountLoginInfo
                        where username = '{givenUserName}'");
                if (givenUserName == registeredUser.UserName)
                {
                    this.VerifyPassword(programContext, registeredUser);
                }
                else
                {
                    Console.WriteLine("invalid username please try again");
                    this.LogginUser(programContext);
                }
            }
        }

        public void VerifyPassword(ProgramContext programContext, User registeredUser)
        {
            Console.WriteLine("Please enter password");
            var givenPassword = Console.ReadLine();

            if (givenPassword == registeredUser.Password)
            {
                // grant access
                programContext.User.AcctID = registeredUser.AcctID;
                programContext.User.UserName = registeredUser.UserName;
                programContext.User.Password = registeredUser.Password;
                programContext.User.LoggedIn = true;
                programContext.UserInterface.RegisteredUserMenu(programContext);
            }
            else
            {
                Console.WriteLine("invalid password please try again");
                this.VerifyPassword(programContext, registeredUser);
            }
        }

        public void ValidateUserIsLoggedIn(ProgramContext programContext)
        {
            if (programContext.User.LoggedIn != true)
            {
                Console.WriteLine("Please Log In");
                programContext.UserInterface.StartUpOptions(programContext);
            }
        }
    }
}
