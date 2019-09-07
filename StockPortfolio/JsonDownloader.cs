using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Avapi.AvapiTIME_SERIES_DAILY_ADJUSTED;
using Avapi;

namespace StockPortfolio
{
    public class JsonDownloader
    {
        public void DownloadJson(string symbol, List<DailyStockRecord> recordList)
        {
            using (var client = new WebClient())
            {
                string RawJson = client.DownloadString("https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol=" + symbol + "&outputsize=full&apikey=XWLLHF7YZER61GTB");

                recordList = JsonConvert.DeserializeObject<List<DailyStockRecord>>(RawJson);

                foreach (DailyStockRecord record in recordList)
                {
                    Console.WriteLine(record.Date + " " + record.Close);
                    Console.ReadLine();
                }
            }
        }

        public void AlphaVantageDownloader(string symbol, DailyStockRecord dailyStockRecord, List<DailyStockRecord> dailyRecordList)
        {
            // Creating the connection object
            IAvapiConnection connection = AvapiConnection.Instance;

            // Set up the connection and pass the API_KEY provided by alphavantage.co
            connection.Connect("Enter Key Here");

            // Get the TIME_SERIES_DAILY query object
            Int_TIME_SERIES_DAILY_ADJUSTED time_series_daily_adjusted =
                connection.GetQueryObject_TIME_SERIES_DAILY_ADJUSTED();

            // Perform the TIME_SERIES_DAILY request and get the result
            IAvapiResponse_TIME_SERIES_DAILY_ADJUSTED time_series_dailyResponse =
            time_series_daily_adjusted.Query(
                 symbol,
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
                foreach (var timeseries in data.TimeSeries)
                {
                    Console.WriteLine("open: " + timeseries.open);
                    Console.WriteLine("high: " + timeseries.high);
                    Console.WriteLine("low: " + timeseries.low);
                    Console.WriteLine("close: " + timeseries.close);
                    Console.WriteLine("volume: " + timeseries.volume);
                    Console.WriteLine("DateTime: " + timeseries.DateTime);
                    Console.WriteLine("Dividend: " + timeseries.dividendamount);
                    Console.WriteLine("========================");

                    dailyStockRecord.Symbol = data.MetaData.Symbol;
                    dailyStockRecord.Open = Convert.ToDecimal(timeseries.open);
                    dailyStockRecord.High = Convert.ToDecimal(timeseries.high);
                    dailyStockRecord.Low = Convert.ToDecimal(timeseries.low);
                    dailyStockRecord.Close = Convert.ToDecimal(timeseries.close);
                    dailyStockRecord.Volume = Convert.ToDecimal(timeseries.volume);
                    dailyStockRecord.Date = Convert.ToDateTime(timeseries.DateTime);
                    dailyStockRecord.Dividend = Convert.ToDecimal(timeseries.dividendamount);

                    //Console.WriteLine(dailyStockRecord.Open);
                    //Console.WriteLine(dailyStockRecord.High);
                    //Console.WriteLine(dailyStockRecord.Low);
                    //Console.WriteLine(dailyStockRecord.Close);
                    //Console.WriteLine(dailyStockRecord.Volume);
                    //Console.WriteLine(dailyStockRecord.Date);
                    //Console.WriteLine(dailyStockRecord.Dividend);

                    dailyRecordList.Add(dailyStockRecord);

                }
            }
        }
    }
}
