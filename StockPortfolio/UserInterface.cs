using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class UserInterface
    {
        public void StartUpOptions(ProgramContext programContext)
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
                        programContext.AddStockInfo.AddAllStocksToDataBase(programContext);
                        break;
                    }

                case "2":
                    {
                        // Research daily records for a stock
                        programContext.DownloadStockHistoricalRecords.DownloadHistoricalStockRecords(programContext);
                        break;
                    }

                case "3":
                    {
                        // Create an account
                        programContext.UserAccountCreator.CreateUserAccount(programContext);
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
                        this.StartUpOptions(programContext);
                        break;
                    }
            }
        }

        public void RegisteredUserMenu(User user)
        {

        }
    }
}
