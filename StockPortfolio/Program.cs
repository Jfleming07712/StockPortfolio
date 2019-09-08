using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using System.Data;

namespace StockPortfolio
{
    class Program
    {
        static void Main(string[] args)
        {
            StockInfo stockInfo = new StockInfo();
            AddStockInfo addStockInfo = new AddStockInfo();
            JsonDownloader jsonDownloader = new JsonDownloader();
            List<DailyStockRecord> dailyRecordList = new List<DailyStockRecord>();
            DailyStockRecord dailyStockRecord = new DailyStockRecord();
            Calculations calculations = new Calculations();

            Console.WriteLine("Which stock would you like to download?  Please enter the symbol");

            var stockToDownload = Console.ReadLine();

            jsonDownloader.AlphaVantageDownloader(stockToDownload, dailyStockRecord, dailyRecordList, calculations);

            calculations.CalcDailyChange(dailyRecordList);
            calculations.CalcHigh52Week(dailyRecordList);
            calculations.CalcLow52Week(dailyRecordList);
            calculations.CalcOverNightChange(dailyRecordList);
            calculations.CalcVolitilityRating(dailyRecordList);

            //dailyRecordList.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            foreach (DailyStockRecord record in dailyRecordList)
            {
                Console.WriteLine("Symbol " +           record.Symbol);
                Console.WriteLine("open: " +            record.Open);
                Console.WriteLine("high: " +            record.High);
                Console.WriteLine("low: " +             record.Low);
                Console.WriteLine("close: " +           record.Close);
                Console.WriteLine("Adj Close " +        record.AdjustedClose);
                Console.WriteLine("volume: " +          record.Volume);
                Console.WriteLine("date: " +            record.Date);
                Console.WriteLine("dividend: " +        record.Dividend);
                Console.WriteLine("52 week high " +     record.High52Week);
                Console.WriteLine("52 week low " +      record.Low52Week);
                Console.WriteLine("overnight change " + record.OverNightChange);
                Console.WriteLine("daily change " +     record.DailyChange);
                Console.WriteLine("volatility " +       record.VolitilityRating);

                Console.ReadLine();
            }




            var sourceFile1 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist1.txt";
            var sourceFile2 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist2.txt";
            var sourceFile3 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist3.txt";

            //addStockInfo.AddStock(stockInfo, sourceFile1);
            //addStockInfo.AddStock(stockInfo, sourceFile2);
            //addStockInfo.AddStock(stockInfo, sourceFile3);
        }
    }
}
