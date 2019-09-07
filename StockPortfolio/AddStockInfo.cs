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

        public void AddStock(StockInfo stockInfo, string source)
        {
            using (var reader = new StreamReader(source))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<StockInfo>();
                foreach (StockInfo record in records)
                {
                    Console.WriteLine(record.Name + " " + record.Symbol + " " + record.MarketCap + " " + record.IPOyear + " " + record.Sector);
                    this.SqlForAddingRecord(record);
                }
            }
        }
        public void SqlForAddingRecord(StockInfo record)
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
    }
}
