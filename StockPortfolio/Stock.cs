using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    ///"Symbol","Name","LastSale","MarketCap","IPOyear","Sector","industry","Summary Quote",
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string LastSale { get; set; }
        public string MarketCap { get; set; }
        public string IPOyear { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string SummaryQuote { get; set; }
        public List<DailyStockRecord> DailyStockRecords { get; set; }

    }
}
