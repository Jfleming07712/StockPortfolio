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
            TransactionRecord transactionRecord = new TransactionRecord();
            Stock stock = new Stock();
            AddStockInfo addStockInfo = new AddStockInfo();
            JsonDownloader jsonDownloader = new JsonDownloader();
            List<DailyStockRecord> dailyRecordList = new List<DailyStockRecord>();
            DailyStockRecord dailyStockRecord = new DailyStockRecord();
            Calculations calculations = new Calculations();
            DownloadStockHistoricalRecords downloadStockHistoricalRecords = new DownloadStockHistoricalRecords();
            UserInterface userInterface = new UserInterface();
            UserAccountCreator userAccountCreator = new UserAccountCreator();
            UserLoggin userLoggin = new UserLoggin();
            List<Portfolio> portfolioList = new List<Portfolio>();
            Portfolio portfolio = new Portfolio();
            User user = new User();
            ProgramContext programContext = new ProgramContext()
            
            {
                TransactionRecord = transactionRecord,
                Stock = stock,
                AddStockInfo = addStockInfo,
                JsonDownloader = jsonDownloader,
                DailyRecordList = dailyRecordList,
                DailyStockRecord = dailyStockRecord,
                Calculations = calculations,
                DownloadStockHistoricalRecords = downloadStockHistoricalRecords,
                UserInterface = userInterface,
                UserAccountCreator = userAccountCreator,
                PortfolioList = portfolioList,
                Portfolio = portfolio,
                UserLoggin = userLoggin,
                User = user,
                SourceFile1 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist1.txt",
                SourceFile2 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist2.txt",
                SourceFile3 = @"c:\users\jflem\source\repos\stockportfolio\stockportfolio\companylist3.txt",
                SourceFileKey = @"C:\Users\jflem\Documents\Notepadstuff\alphavantageapikey.txt"
            };

            programContext.AlphaVantageKey = File.ReadAllText(@"C:\Users\jflem\Documents\Notepadstuff\alphavantageapikey.txt");

            

            // test section


            //Console.ReadLine();
            // end test section

            userInterface.StartUpOptions(programContext);

        }
    }
}
