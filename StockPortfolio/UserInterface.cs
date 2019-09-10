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

        public void RegisteredUserMenu(ProgramContext programContext)
        {
            programContext.UserLoggin.ValidateUserIsLoggedIn(programContext);

            string tempUserInput;

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Update database with with all company info");
            Console.WriteLine("2) Research daily records for a stock");
            Console.WriteLine("3) Create a portfolio");
            Console.WriteLine("4) Delete a portfolio");
            Console.WriteLine("5) Update records for stocks in your portfolio");
            Console.WriteLine("6) View portfolio");
            Console.WriteLine("7) View Stocks in your portfolio");
            Console.WriteLine("8) Deposit or withdraw cash");
            Console.WriteLine("9) View cash account");
            Console.WriteLine("10) View cash transaction record");
            Console.WriteLine("11) Log out");

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
                        // Create a portfolio
                        break;
                    }

                case "4":
                    {
                        // Delete a portfolio
                        break;
                    }

                case "5":
                    {
                        // Update records for stocks in portfolio
                        break;
                    }

                case "6":
                    {
                        // View Portfolio
                        break;
                    }

                case "7":
                    {
                        // View stocks in portfolio
                        break;
                    }

                case "8":
                    {
                        // deposit or withdraw cash
                        break;
                    }

                case "9":
                    {
                        // View cash account
                        break;
                    }

                case "10":
                    {
                        // view cash transaction record
                        break;
                    }

                case "11":
                    {
                        // Log out
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
    }
}
