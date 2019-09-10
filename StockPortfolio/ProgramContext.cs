using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class ProgramContext
    {
        public Stock Stock { get; set; }
        public AddStockInfo AddStockInfo { get; set; }
        public JsonDownloader JsonDownloader { get; set; }
        public List<DailyStockRecord> DailyRecordList { get; set; }
        public DailyStockRecord DailyStockRecord { get; set; }
        public Calculations Calculations { get; set; }
        public DownloadStockHistoricalRecords DownloadStockHistoricalRecords { get; set; }
        public UserInterface UserInterface { get; set; }
        public UserAccountCreator UserAccountCreator { get; set; }
        public User User { get; set; }
        public UserLoggin UserLoggin { get; set; }
        public string SourceFile1 { get; set; }
        public string SourceFile2 { get; set; }
        public string SourceFile3 { get; set; }
        public string SourceFileKey { get; set; }
        public string AlphaVantageKey { get; set; }
        public string Symbol { get; set; }




    }
}
