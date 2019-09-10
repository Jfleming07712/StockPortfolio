using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class DownloadStockHistoricalRecords
    {
        public void DownloadHistoricalStockRecords(ProgramContext programContext)
        {
            Console.WriteLine("Which stock would you like to download?  Please enter the symbol");

            var stockToDownload = Console.ReadLine();

            programContext.JsonDownloader.AlphaVantageDownloader(programContext);

            programContext.Calculations.CalculationsForDailyRecord(programContext);

            programContext.AddStockInfo.SqlForAddingDailyRecord(programContext);

            programContext.UserInterface.StartUpOptions(programContext);
        }

        
    }
}
