using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockPortfolio
{
    public class TransactionRecord
    {
        public int PortfolioID { get; set; }
        public int TransactionID { get; set; }
        public int StockID { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal TransactionFees { get; set; }





        public void BuySellStock(ProgramContext programContext)
        {
            string portfolioName;
            int portfolioID;
            TransactionRecord transactionToBeAdded = new TransactionRecord();
            decimal transactionFees = 0;
            decimal price;
            DateTime dateTime;
            decimal quantity;
            string symbol;
            string tempStockID;

            Console.WriteLine("Please enter the symbol for the stock you would like to buy or sell");
            symbol = Console.ReadLine();

            Console.WriteLine("Please enter the name of the portfolio this stock is in");
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                var tempPortfolioList = connection.Query<Portfolio>(
    $@"declare @UserAcctID int
set @UserAcctID = '{programContext.User.AcctID}'
select *
from Portfolio
where AcctID = @UserAcctID").ToList();

                foreach (Portfolio portfolio in tempPortfolioList)
                {
                    Console.WriteLine(portfolio.PortfolioName);
                }
            }
            portfolioName = Console.ReadLine();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                portfolioID = connection.QuerySingle<int>(
    $@"declare @PortfolioName nvarchar(max) = '{portfolioName}'
Select PortfolioID
from Portfolio
where PortfolioName = @PortfolioName");
            }

            Console.WriteLine("Please enter the quantity of share you would like to buy or sell");
            Console.WriteLine("use a - sign to indicate selling");
            quantity = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Please enter the date the stock was bought or sold.  Use year,Month,Day format");
            dateTime = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Please enter the price you bought or sold the shares for");
            price = Convert.ToDecimal(Console.ReadLine());

            transactionToBeAdded.TransactionFees = this.TransactionFeeSelector(programContext, transactionFees, price, quantity);

            transactionToBeAdded.PortfolioID = portfolioID;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                tempStockID = connection.QuerySingle<string>(
    $@"Declare @symbol nchar(10) = '{symbol}'
Select StockID
from Stock
where Symbol = @symbol");
            }

            transactionToBeAdded.StockID = Convert.ToInt32(tempStockID);
            transactionToBeAdded.DateTime = dateTime;
            transactionToBeAdded.Price = price;
            transactionToBeAdded.Quantity = quantity;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                connection.Query(
    $@"Declare @PortfolioID int = '{transactionToBeAdded.PortfolioID}',
@StockID int = '{transactionToBeAdded.StockID}',
@DateTime DateTime = '{transactionToBeAdded.DateTime}',
@Price decimal(18,6) = '{transactionToBeAdded.Price}',
@Quantity decimal(18,6) = '{transactionToBeAdded.Quantity}',
@Fees decimal(18,6) = '{transactionToBeAdded.TransactionFees}',
@TransactionFee decimal(18,6) = '{transactionToBeAdded.TransactionFees}'
Insert Into TransactionRecord (StockID, DateTime, Price, Quantity, Fees, PortfolioID)
values (@StockID, @DateTime, @Price, @Quantity, @Fees, @PortfolioID)");
            }
        }

        public decimal TransactionFeeSelector(ProgramContext programContext, decimal transactionFees, decimal price, decimal quantity)
        {
            var tempQuantity = quantity;

            Console.WriteLine("How are the transaction fees being calculated?");
            Console.WriteLine("Enter 1 for a percentage");
            Console.WriteLine("Enter 2 for a flat dollar amount");
            var selection = Console.ReadLine();

            switch (selection)
            {
                case "1":
                    {
                        Console.WriteLine("Please enter the fee percentage as a decimal.  I.E. 2.5% is .025");
                        decimal percentage = Convert.ToDecimal(Console.ReadLine());

                        if (quantity < 0)
                        {
                            tempQuantity = quantity * -1;
                        }

                        transactionFees = price * tempQuantity * percentage;
                        break;
                    }

                case "2":
                    {
                        Console.WriteLine("Please enter the flat fee as a decimal.");
                        decimal flatFee = Convert.ToDecimal(Console.ReadLine());

                        if (quantity < 0)
                        {
                            tempQuantity = quantity * -1;
                        }

                        transactionFees = price * tempQuantity + flatFee;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Not a valid entry");
                        this.TransactionFeeSelector(programContext, transactionFees, price, quantity);
                        break;
                    }
            }
            return transactionFees;
        }
    }
}

