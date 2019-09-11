using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockPortfolio
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public string PortfolioName { get; set; }
        public int AcctID { get; set; }
        public decimal PlYtd { get; set; }
        public decimal PlDaily { get; set; }
        public decimal StockWorth { get; set; }
        public decimal CashBalance { get; set; }
        public decimal PortfolioWorth { get; set; }
        public List<Stock> Stocks { get; set; }



        public void CreatePortfolio(ProgramContext programContext)
        {
            Console.WriteLine("Please name your portfolio");
            var enteredPortfolioName = Console.ReadLine();

            if (string.IsNullOrEmpty(enteredPortfolioName))
            {
                Console.WriteLine("Invalid name, Please try again");
                this.CreatePortfolio(programContext);
            }
            else
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
                {
                    var ownedPortfolioNames = connection.Query<User>(
$@"select username
from AccountLoginInfo
where username = '{enteredPortfolioName}'").ToList();
                    if (ownedPortfolioNames.Count != 0)
                    {
                        Console.WriteLine("Username is in use please pick another");
                        programContext.Portfolio.CreatePortfolio(programContext);
                    }
                    else
                    {
                        programContext.Portfolio.PortfolioName = enteredPortfolioName;

                        programContext.Portfolio.AcctID = programContext.User.AcctID;

                        programContext.PortfolioList.Add(programContext.Portfolio);

                        this.AddPortfolioToDatabase(programContext, enteredPortfolioName);
                    }
                }
            }
        }

        public void AddPortfolioToDatabase(ProgramContext programContext, string enteredPortfolioName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                var ownedPortfolioNames = connection.Query(
$@"declare @portfolioName nvarchar(10)
declare @AcctID int
set @portfolioName = '{enteredPortfolioName}'
set @AcctID = '{programContext.User.AcctID}'
insert into AccountLoginInfo (Username, Password)
values (@portfolioName, @AcctID);");
            }
        }
    }
}
