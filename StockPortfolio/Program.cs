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

            Console.WriteLine("Which stock would you like to download?  Please enter the symbol");

            var stockToDownload = Console.ReadLine();

            jsonDownloader.AlphaVantageDownloader(stockToDownload, dailyStockRecord, dailyRecordList);


            Console.WriteLine("Number of entries:  " + dailyRecordList.Count);
            Console.ReadLine();




            var sourceFile1 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist1.txt";
            var sourceFile2 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist2.txt";
            var sourceFile3 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist3.txt";

            //addStockInfo.AddStock(stockInfo, sourceFile1);
            //addStockInfo.AddStock(stockInfo, sourceFile2);
            //addStockInfo.AddStock(stockInfo, sourceFile3);
        }
    }
}
