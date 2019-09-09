using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class TestMethods
    {
        public void PrintDailyRecordInfo(List<DailyStockRecord> dailyRecordList)
        {
            dailyRecordList.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            foreach (DailyStockRecord record in dailyRecordList)
            {
                Console.WriteLine("Symbol " + record.Symbol);
                Console.WriteLine("open: " + record.Open);
                Console.WriteLine("high: " + record.High);
                Console.WriteLine("low: " + record.Low);
                Console.WriteLine("close: " + record.Close);
                Console.WriteLine("Adj Close " + record.AdjustedClose);
                Console.WriteLine("volume: " + record.Volume);
                Console.WriteLine("date: " + record.Date);
                Console.WriteLine("dividend: " + record.Dividend);
                Console.WriteLine("52 week high " + record.High52Week);
                Console.WriteLine("52 week low " + record.Low52Week);
                Console.WriteLine("overnight change " + record.OverNightChange);
                Console.WriteLine("daily change " + record.DailyChange);
                Console.WriteLine("volatility " + record.VolitilityRating);
                Console.WriteLine("Dividend Yeild " + record.DividendYield);

                Console.ReadLine();
            }
        }
    }
}
