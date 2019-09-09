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
            DownloadStockHistoricalRecords downloadStockHistoricalRecords = new DownloadStockHistoricalRecords();
            UserInterface userInterface = new UserInterface();
            UserAccountCreator userAccountCreator = new UserAccountCreator();
            User user = new User();

            var sourceFile1 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist1.txt";
            var sourceFile2 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist2.txt";
            var sourceFile3 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist3.txt";

            // test section


            //Console.ReadLine();
            // end test section

            userInterface.StartUpOptions(dailyStockRecord, dailyRecordList, addStockInfo, stockInfo, calculations, jsonDownloader, downloadStockHistoricalRecords, sourceFile1, sourceFile2, sourceFile3, userAccountCreator, user, userInterface);

        }
    }
}
