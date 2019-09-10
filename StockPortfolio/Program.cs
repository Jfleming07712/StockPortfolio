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
            Stock stock = new Stock();
            AddStockInfo addStockInfo = new AddStockInfo();
            JsonDownloader jsonDownloader = new JsonDownloader();
            List<DailyStockRecord> dailyRecordList = new List<DailyStockRecord>();
            DailyStockRecord dailyStockRecord = new DailyStockRecord();
            Calculations calculations = new Calculations();
            DownloadStockHistoricalRecords downloadStockHistoricalRecords = new DownloadStockHistoricalRecords();
            UserInterface userInterface = new UserInterface();
            UserAccountCreator userAccountCreator = new UserAccountCreator();
            User user = new User();
            ProgramContext programContext = new ProgramContext();

            programContext.SourceFile1 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist1.txt";
            programContext.SourceFile2 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist2.txt";
            programContext.SourceFile3 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist3.txt";
            programContext.SourceFileKey = @"C:\Users\jflem\Documents\Notepad stuff\alpha vantage api key.txt";

            using (var reader = new StreamReader(programContext.SourceFileKey))
            {
                programContext.AlphaVantageKey = reader.ReadLine();
            }


            // test section


            //Console.ReadLine();
            // end test section

            userInterface.StartUpOptions(programContext);

        }
    }
}
