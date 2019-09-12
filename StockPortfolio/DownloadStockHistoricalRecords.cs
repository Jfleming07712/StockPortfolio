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

            programContext.Symbol = Console.ReadLine();

            this.DownloadHistoricalStockRecords(programContext);
        }
        public void DownloadHistoricalStockRecords(ProgramContext programContext)
        {
            programContext.JsonDownloader.AlphaVantageDownloader(programContext);

            programContext.Calculations.CalculationsForDailyRecord(programContext);

            programContext.AddStockInfo.SqlForAddingDailyRecord(programContext);

            programContext.UserInterface.StartUpOptions(programContext);
        }

        public void UpdateStocksInPortfolio(ProgramContext programContext)
        {
            IEnumerable<string> bigSymbolList = null;
            string portfolioIDsToSearch = null;
            IEnumerable<string> symbols;
            IEnumerable<int> portfolioIDList;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
              portfolioIDList = connection.Query<int>(
                    $@"Select PortfolioID from Portfolio p where AcctID = '{programContext.User.AcctID}'");
            }

            foreach (int portfolioID in portfolioIDList)
            {
                using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
                {
                    symbols = connection.Query<string>(
                          $@"Select symbol from TransactionRecord where PortfolioID = '{portfolioID}'"); //NEED TO LOOK AT THIS AGAIN.  

                    bigSymbolList = (bigSymbolList ?? Enumerable.Empty<string>()).Concat(symbols ?? Enumerable.Empty<string>());
                }
            }

            bigSymbolList = bigSymbolList.Distinct().ToList();

            foreach (string symbol in bigSymbolList)
            {
                programContext.Symbol = symbol;
                this.DownloadHistoricalStockRecords(programContext);
            }
            ///////////////////////////////
            Console.ReadLine();
            ///////////////////////////////

            
        }
    }
}
