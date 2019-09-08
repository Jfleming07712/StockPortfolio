using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class DailyStockRecord
    {
        public int DailyRecordID { get; set; }
        public int StockID { get; set; }
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal AdjustedClose { get; set; }
        public decimal Volume { get; set; }
        public decimal High52Week { get; set; }
        public decimal Low52Week { get; set; }
        public decimal OverNightChange { get; set; }
        public decimal DailyChange { get; set; }
        public double VolitilityRating { get; set; }
        public decimal Dividend { get; set; }
    }
}
