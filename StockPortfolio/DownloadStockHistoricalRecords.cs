using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class DownloadStockHistoricalRecords
    {
        public void DownloadHistoricalStockRecords(DailyStockRecord dailyStockRecord, List<DailyStockRecord> dailyRecordList, Calculations calculations, JsonDownloader jsonDownloader, AddStockInfo addStockInfo)
        {
            Console.WriteLine("Which stock would you like to download?  Please enter the symbol");

            var stockToDownload = Console.ReadLine();

            jsonDownloader.AlphaVantageDownloader(stockToDownload, dailyStockRecord, dailyRecordList, calculations);

            calculations.CalculationsForDailyRecord(dailyRecordList);

            addStockInfo.SqlForAddingDailyRecord(dailyRecordList);
        }

        
    }
}
