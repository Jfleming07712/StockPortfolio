using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Dapper;
using CsvHelper;
using System.IO;
namespace StockPortfolio
{
    public class AddStockInfo
    {

        public void AddStock(string source)
        {
            using (var reader = new StreamReader(source))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<Stock>();
                foreach (Stock record in records)
                {
                    Console.WriteLine(record.Name + " " + record.Symbol + " " + record.MarketCap + " " + record.IPOyear + " " + record.Sector);
                    this.SqlForAddingRecord(record);
                }
            }
        }
        public void SqlForAddingRecord(Stock record)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))
            {
                connection.Query(
                    $@"MERGE stock AS [target]
                    USING (SELECT '{record.Symbol}' AS symbol) AS [source]
                    ON [target].symbol = [source].symbol
                    WHEN NOT MATCHED THEN INSERT
                    (CompanyName, Symbol, MarketCap, IpoYear, Sector)
                    VALUES ('{record.Name}', '{record.Symbol}', '{record.MarketCap}', '{record.IPOyear}', '{record.Sector}');");
            }
        }

        public void SqlForAddingDailyRecord(List<DailyStockRecord> dailyStockRecords)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))

                foreach (DailyStockRecord record in dailyStockRecords)
                {
                    connection.Query(
                        $@"INSERT INTO DailyRecord
                        (StockID, Symbol, RecordDate, OpenPrice, DailyHigh, DailyLow, ClosePrice, AdjustedClose, Volume, High52Week, Low52Week, OverNightChange, 
                        DailyChange, VolatilityRating, DividendYield)
                        Select StockID, '{record.Symbol}', '{record.Date}', '{record.Open}', '{record.High}', '{record.Low}', '{record.Close}', '{record.AdjustedClose}',
                        '{record.Volume}', '{record.High52Week}', '{record.Low52Week}', '{record.OverNightChange}', '{record.DailyChange}', '{record.VolitilityRating}',
                        '{record.DividendYield}' from Stock where Symbol = '{record.Symbol}';");
                }
        }

        public void AddAllStocksToDataBase(Stock stock, string sourceFile1, string sourceFile2, string sourceFile3)
        {
            this.AddStock(sourceFile1);
            this.AddStock(sourceFile2);
            this.AddStock(sourceFile3);
        }
    }
}
