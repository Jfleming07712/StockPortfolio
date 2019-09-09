using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class UserInterface
    {
        public void StartUpOptions(DailyStockRecord dailyStockRecord, List<DailyStockRecord> dailyRecordList, AddStockInfo addStockInfo, Stock stockInfo, Calculations calculations, JsonDownloader jsonDownloader, DownloadStockHistoricalRecords downloadStockHistoricalRecords, string sourceFile1, string sourceFile2, string sourceFile3, UserAccountCreator userAccountCreator, User user, UserInterface userInterface)
        {
            string tempUserInput;

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Update database with with all company info");
            Console.WriteLine("2) Research daily records for a stock");
            Console.WriteLine("3) Create an account");
            Console.WriteLine("4) Log into an account");
            Console.WriteLine("Please enter the number of your selection");

            tempUserInput = Console.ReadLine();

            switch (tempUserInput)
            {
                case "1":
                    {
                        // update database with all company info
                        addStockInfo.AddAllStocksToDataBase(stockInfo, sourceFile1, sourceFile2, sourceFile3);
                        break;
                    }

                case "2":
                    {
                        // Research daily records for a stock
                        downloadStockHistoricalRecords.DownloadHistoricalStockRecords(dailyStockRecord, dailyRecordList, calculations, jsonDownloader, addStockInfo);
                        break;
                    }

                case "3":
                    {
                        // Create an account
                        userAccountCreator.CreateUserAccount(user, userInterface, dailyStockRecord, dailyRecordList,addStockInfo,stockInfo, calculations,jsonDownloader,downloadStockHistoricalRecords,sourceFile1, sourceFile2, sourceFile3, userAccountCreator);
                        break;
                    }

                case "4":
                    {
                        // Log into an account
                        break;
                    }

                default:
                    {
                        // invalid entry
                        Console.WriteLine("Invalid entry, please try again.");
                        this.StartUpOptions(dailyStockRecord, dailyRecordList, addStockInfo, stockInfo, calculations, jsonDownloader, downloadStockHistoricalRecords, sourceFile1, sourceFile2, sourceFile3, userAccountCreator, user, userInterface);
                        break;
                    }
            }

        }
    }
}
