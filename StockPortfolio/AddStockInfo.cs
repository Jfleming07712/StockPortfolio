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
                //csv.Configuration.RegisterClassMap<StockMap>();
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
$@"
declare @symbol nvarchar(10) = '{record.Symbol}',
@name nvarchar(max) = '{record.Name}',
@marketCap nvarchar(max) = '{record.MarketCap}',
@IPOyear nvarchar(max) = '{record.IPOyear}',
@sector nvarchar(max) = '{record.Sector}'

MERGE stock AS [target]
USING (SELECT '{record.Symbol}' AS symbol) AS [source]
ON [target].symbol = [source].symbol
WHEN NOT MATCHED THEN INSERT
(CompanyName, Symbol, MarketCap, IpoYear, Sector)
VALUES (@name, @symbol, @marketCap, @IPOyear, @sector);");
            }
        }

        public void SqlForAddingDailyRecord(ProgramContext programContext)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("StockPortfolio")))

                foreach (DailyStockRecord record in programContext.DailyRecordList)
                {
                    connection.Query(
$@"declare @symbol nvarchar(10) = '{record.Symbol}',
@date Date = '{record.Date}',
@high decimal(18,6) = '{record.High}',
@low decimal(18,6) = '{record.Low}',
@close decimal(18,6) = '{record.Close}',
@adjustedClose decimal(18,6) = '{record.AdjustedClose}',
@volume decimal(18,6) = '{record.Volume}',
@high52Week decimal(18,6) = '{record.High52Week}',
@low52Week decimal(18,6) = '{record.Low52Week}',
@overNightChange decimal(18,6) = '{record.OverNightChange}',
@dailyChange decimal(18,6) = '{record.DailyChange}',
@volitilityRating decimal(18,6) = '{record.VolitilityRating}',
@dividendYield decimal(18,6) = '{record.DividendYield}',

INSERT INTO DailyRecord
(StockID, Symbol, RecordDate, OpenPrice, DailyHigh, DailyLow, ClosePrice, AdjustedClose, Volume, High52Week, Low52Week, OverNightChange, 
DailyChange, VolatilityRating, DividendYield)
Select StockID, @symbol, @date, @open, @high, @low, @close, @adjustedClose,
@volume, @high52Week, @low52Week, @overNightChange, @dailyChange, @volitilityRating,
@dividendYield from Stock where Symbol = '@symbol;");
                }
        }

        public void AddAllStocksToDataBase(ProgramContext programContext)
        {
            this.AddStock(programContext.SourceFile1);
            this.AddStock(programContext.SourceFile2);
            this.AddStock(programContext.SourceFile3);
        }
    }
}
