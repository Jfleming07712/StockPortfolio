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
                    var ownedPortfolioNames = connection.Query(
$@"select PortfolioName
from Portfolio
where PortfolioName = '{enteredPortfolioName}'").ToList();

                    ///////////////

                    if (ownedPortfolioNames.Count != 0)
                    {
                        Console.WriteLine("Username is in use please pick another");
                        programContext.Portfolio.CreatePortfolio(programContext);
                    }
                    else
                    {
                        programContext.Portfolio.PortfolioName = enteredPortfolioName;

                        programContext.Portfolio.AcctID = programContext.User.AcctID;

                        //programContext.PortfolioList.Add(programContext.Portfolio);

                        this.AddPortfolioToDatabase(programContext, enteredPortfolioName);

                        this.UpdatePortfolioList(programContext);
                    }
                }
                Console.WriteLine("Your portfolios");
                foreach (Portfolio portfolio in programContext.PortfolioList)
                {
                    Console.WriteLine(portfolio.PortfolioName);
                }
                Console.ReadLine();
                programContext.UserInterface.RegisteredUserMenu(programContext);
            }
        }

        public void AddPortfolioToDatabase(ProgramContext programContext, string enteredPortfolioName)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                var ownedPortfolioNames = connection.Query(
$@"declare @portfolioName nvarchar(max)
declare @AcctID int
set @portfolioName = '{enteredPortfolioName}'
set @AcctID = '{programContext.User.AcctID}'
insert into Portfolio (PortfolioName, AcctID)
values (@portfolioName, @AcctID);");
            }
        }

        //  BIG PROBLEMS HERE BUT i AM TOO TIRED TO KEEP GOING
        public void UpdatePortfolioList(ProgramContext programContext)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                var tempPortfolioList = connection.Query(
$@"select *
from Portfolio
where AcctID = '{programContext.User.AcctID}'").ToList();

                foreach (var portfolio in tempPortfolioList)
                {
                    programContext.PortfolioList.Add(portfolio);
                }
            }
            
        }
    }
}
