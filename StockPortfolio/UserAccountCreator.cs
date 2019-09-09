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
        public void CreateUserAccount(User user, UserInterface userInterface, DailyStockRecord dailyStockRecord, List<DailyStockRecord> dailyRecordList, AddStockInfo addStockInfo, Stock stockInfo, Calculations calculations, JsonDownloader jsonDownloader, DownloadStockHistoricalRecords downloadStockHistoricalRecords, string sourceFile1, string sourceFile2, string sourceFile3, UserAccountCreator userAccountCreator)
        {
            this.CreateUserName(user);
            this.CreateUserPassword(user);
            this.AddUserToDataBase(user);
            userInterface.StartUpOptions(dailyStockRecord, dailyRecordList, addStockInfo, stockInfo, calculations, jsonDownloader, downloadStockHistoricalRecords, sourceFile1, sourceFile2, sourceFile3, userAccountCreator, user, userInterface);
        }
        public void CreateUserName(User user)
        {
            Console.WriteLine("Please create a Username");
            var tempUserName = Console.ReadLine();

            if (string.IsNullOrEmpty(tempUserName))
            {
                // invalid entry
                Console.WriteLine("Invalid entry please try again");
                this.CreateUserName(user);
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
                        this.CreateUserName(user);
                    }
                    else
                    {
                        user.UserName = tempUserName;
                    }
                }
            }
        }

        public void CreateUserPassword(User user)
        {
            Console.WriteLine("Please create a password");
            var tempUserPassword = Console.ReadLine();

            if (string.IsNullOrEmpty(tempUserPassword))
            {
                // invalid entry
                Console.WriteLine("Invalid entry please try again");
                this.CreateUserPassword(user);
            }
            else
            {
                user.Password = tempUserPassword;
            }
        }

        public void AddUserToDataBase(User user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                connection.Query(
$@"declare @username nvarchar(max)
declare @password nvarchar(max)
set @username = '{user.UserName}'
set @password = '{user.Password}'
insert into AccountLoginInfo (Username, Password)
values (@username, @password)");
            }
        }
    }
}
