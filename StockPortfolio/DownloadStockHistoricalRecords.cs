using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace StockPortfolio
{
    public class DownloadStockHistoricalRecords
    {
        public void DownloadHistoricalStockRecordsUI(ProgramContext programContext)
        {
            Console.WriteLine("Which stock would you like to download?  Please enter the symbol");

            var symbol = Console.ReadLine();

            this.DownloadHistoricalStockRecords(programContext, symbol);
        }
        public void DownloadHistoricalStockRecords(ProgramContext programContext, string symbol)
        {
            programContext.JsonDownloader.AlphaVantageDownloader(programContext, symbol);

            programContext.Calculations.CalculationsForDailyRecord(programContext);

            programContext.AddStockInfo.SqlForAddingDailyRecord(programContext);

            programContext.UserInterface.StartUpOptions(programContext);
        }

        public void UpdateStocksInPortfolio(ProgramContext programContext)
        {
            IEnumerable<string> bigSymbolList = null;
            IEnumerable<string> symbols;
            IEnumerable<int> portfolioIDList;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
              portfolioIDList = connection.Query<int>(
$@"declare @AcctID int = '{programContext.User.AcctID}'
Select PortfolioID from Portfolio p where AcctID = @AcctID");
            }

            foreach (int portfolioID in portfolioIDList)
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
                {
                    symbols = connection.Query<string>(
$@"declare @portfolioID int = '{portfolioID}'
Select symbol from TransactionRecord tr join Stock s on s.StockID = tr.StockID where PortfolioID = @portfolioID");  

                    bigSymbolList = (bigSymbolList ?? Enumerable.Empty<string>()).Concat(symbols ?? Enumerable.Empty<string>());
                }
            }

            bigSymbolList = bigSymbolList.Distinct().ToList();

            foreach (string symbol in bigSymbolList)
            {
                var symbolToUpdate = symbol;
                this.DownloadHistoricalStockRecords(programContext, symbol);
            }
        }
    }
}
