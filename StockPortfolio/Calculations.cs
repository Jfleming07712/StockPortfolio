using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockPortfolio
{
    public class Calculations
    {
        public void CalcHigh52Week(List<DailyStockRecord> dailyRecordList)
        {
            var queryList = dailyRecordList.ToList();

            foreach (DailyStockRecord record in dailyRecordList)
            {
                record.High52Week = queryList.Where(x => x.Date > record.Date.AddYears(-1) && x.Date <= record.Date).Select(x => x.High).Max();
            }
        }

        public void CalcLow52Week(List<DailyStockRecord> dailyRecordList)
        {
            var queryList = dailyRecordList.ToList();

            foreach (DailyStockRecord record in dailyRecordList)
            {
                record.Low52Week = queryList.Where(x => x.Date > record.Date.AddYears(-1) && x.Date <= record.Date).Select(x => x.Low).Min();
            }
        }

        public void CalcOverNightChange (List<DailyStockRecord> dailyRecordList)
        {
            dailyRecordList.OrderByDescending(x => x.Date);

            foreach (DailyStockRecord record in dailyRecordList)
            {
                if (dailyRecordList.IndexOf(record) != dailyRecordList.Count - 1)
                {
                    record.OverNightChange = record.Open - dailyRecordList[dailyRecordList.IndexOf(record) + 1].Close;
                }
            }
        }

        public void CalcDailyChange(List<DailyStockRecord> dailyRecordList)
        {
            dailyRecordList.OrderByDescending(x => x.Date);

            foreach (DailyStockRecord record in dailyRecordList)
            {
                if (dailyRecordList.IndexOf(record) != dailyRecordList.Count - 1)
                {
                    record.DailyChange = record.Close - record.Open;
                }
            }
        }

        public void CalcVolitilityRating(List<DailyStockRecord> dailyRecordList)
        {

            
            List<DailyStockRecord> tempDailyRecordList = new List<DailyStockRecord>();

            var baseDailyRecordList = dailyRecordList.OrderBy(x => x.Date);

            //dailyRecordList.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            foreach (DailyStockRecord record in baseDailyRecordList)
            {
                decimal sum = 0;
                decimal average = 0;
                decimal p = 0;
                decimal variance = 0;
                decimal sumOfP = 0;

                tempDailyRecordList.Add(record);

                foreach (DailyStockRecord tempRecord in tempDailyRecordList)
                {
                    sum += tempRecord.Close;
                }

                average = sum / tempDailyRecordList.Count;

                foreach (DailyStockRecord tempRecord in tempDailyRecordList)
                {
                    p = (tempRecord.Close - average) * (tempRecord.Close - average);
                    sumOfP += p;
                }

                variance = sumOfP / tempDailyRecordList.Count - 1;

                var standardDeviation = Math.Sqrt((double)variance);

                //var volatility = standardDeviation * 100;

                record.VolitilityRating = standardDeviation;
                if (Double.IsNaN(record.VolitilityRating))
                {
                    record.VolitilityRating = -1;
                }
            }
        }

        public void CalcDividendYield(List<DailyStockRecord> dailyRecordList)
        {
            var queryList = dailyRecordList.ToList();

            foreach (DailyStockRecord record in dailyRecordList)
            {
                var dividendYield = queryList.Where(x => x.Date > record.Date.AddYears(-1) && x.Date <= record.Date).Select(x => x.Dividend).Sum() / record.Close;
                dividendYield = dividendYield * 100;
                record.DividendYield = decimal.Round(dividendYield, 2, MidpointRounding.AwayFromZero);
            }
        }
    }
}
