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
            FileDownloader fileDownloader = new FileDownloader();

            fileDownloader.DownloadFile("tesla");



            var sourceFile1 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist1.txt";
            var sourceFile2 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist2.txt";
            var sourceFile3 = @"C:\Users\jflem\source\repos\StockPortfolio\StockPortfolio\companylist3.txt";

            //addStockInfo.AddStock(stockInfo, sourceFile1);
            //addStockInfo.AddStock(stockInfo, sourceFile2);
            //addStockInfo.AddStock(stockInfo, sourceFile3);
        }
    }
}
