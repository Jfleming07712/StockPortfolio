using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED;
using Avapi;
using System.Linq;

namespace StockPortfolio
{
    public class JsonDownloader
    {
        public void DownloadJson(ProgramContext programContext, List<DailyStockRecord> recordList)
        {
            using (var client = new WebClient())
            {
                string RawJson = client.DownloadString("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=" + programContext.Symbol + programContext.AlphaVantageKey);

                recordList = JsonConvert.DeserializeObject<List<DailyStockRecord>>(RawJson);

                foreach (DailyStockRecord record in recordList)
                {
                    Console.WriteLine(record.Date + " " + record.Close);
                    Console.ReadLine();
                }
            }
        }

        public void AlphaVantageDownloader(ProgramContext programContext)
        {
            // Creating the connection object
            IAvapiConnection connection = AvapiConnection.Instance;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect(programContext.AlphaVantageKey);

            // Get the TIME_SERIES_DAILY query object
            Int_TIME_SERIES_DAILY_ADJUSTED time_series_daily_adjusted =
                connection.GetQueryObject_TIME_SERIES_DAILY_ADJUSTED();

            // Perform the TIME_SERIES_DAILY request and get the result
            IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED time_series_dailyResponse =
            time_series_daily_adjusted.Query(
                 programContext.Symbol,
                 Const_TIME_SERIES_DAILY_ADJUSTED.TIME_SERIES_DAILY_ADJUSTED_outputsize.full);

            // Printout the results
            Console.WriteLine("******** RAW DATA TIME_SERIES_DAILY ********");
            Console.WriteLine(time_series_dailyResponse.RawData);

            Console.WriteLine("******** STRUCTURED DATA TIME_SERIES_DAILY ********");
            var data = time_series_dailyResponse.Data;
            if (data.Error)
            {
                Console.WriteLine(data.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Information: " + data.MetaData.Information);
                Console.WriteLine("Symbol: " + data.MetaData.Symbol);
                Console.WriteLine("LastRefreshed: " + data.MetaData.LastRefreshed);
                Console.WriteLine("OutputSize: " + data.MetaData.OutputSize);
                Console.WriteLine("TimeZone: " + data.MetaData.TimeZone);
                Console.WriteLine("========================");
                Console.WriteLine("========================");

                //data.TimeSeries.OrderBy(x => x.DateTime);   //testing this out
                foreach (var timeseries in data.TimeSeries)
                {
                    DailyStockRecord tempDailyStockRecord = new DailyStockRecord();

                    Console.WriteLine("open: " + timeseries.open);
                    Console.WriteLine("high: " + timeseries.high);
                    Console.WriteLine("low: " + timeseries.low);
                    Console.WriteLine("close: " + timeseries.close);
                    Console.WriteLine("volume: " + timeseries.volume);
                    Console.WriteLine("DateTime: " + timeseries.DateTime);
                    Console.WriteLine("Dividend: " + timeseries.dividendamount);
                    Console.WriteLine("========================");

                    
                    
                    tempDailyStockRecord.Symbol = data.MetaData.Symbol;
                    tempDailyStockRecord.Open = Convert.ToDecimal(timeseries.open);
                    tempDailyStockRecord.High = Convert.ToDecimal(timeseries.high);
                    tempDailyStockRecord.Low = Convert.ToDecimal(timeseries.low);
                    tempDailyStockRecord.Close = Convert.ToDecimal(timeseries.close);
                    tempDailyStockRecord.Volume = Convert.ToDecimal(timeseries.volume);
                    tempDailyStockRecord.Date = Convert.ToDateTime(timeseries.DateTime);
                    tempDailyStockRecord.Dividend = Convert.ToDecimal(timeseries.dividendamount);
                    tempDailyStockRecord.AdjustedClose = Convert.ToDecimal(timeseries.adjustedclose);


                    programContext.DailyRecordList.Add(tempDailyStockRecord);


                    //tempDailyStockRecord.VolitilityRating = calculations.CalcVolitilityRating(dailyRecordList);


                }
            }
        }
    }
}
