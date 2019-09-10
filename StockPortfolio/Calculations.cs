using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockPortfolio
{
    public class Calculations
    {
        public void CalcHigh52Week(ProgramContext programContext)
        {
            var queryList = programContext.DailyRecordList.ToList();

            foreach (DailyStockRecord record in programContext.DailyRecordList)
            {
                record.High52Week = queryList.Where(x => x.Date > record.Date.AddYears(-1) && x.Date <= record.Date).Select(x => x.High).Max();
            }
        }

        public void CalcLow52Week(ProgramContext programContext)
        {
            var queryList = programContext.DailyRecordList.ToList();

            foreach (DailyStockRecord record in programContext.DailyRecordList)
            {
                record.Low52Week = queryList.Where(x => x.Date > record.Date.AddYears(-1) && x.Date <= record.Date).Select(x => x.Low).Min();
            }
        }

        public void CalcOverNightChange (ProgramContext programContext)
        {
            programContext.DailyRecordList.OrderByDescending(x => x.Date);

            foreach (DailyStockRecord record in programContext.DailyRecordList)
            {
                if (programContext.DailyRecordList.IndexOf(record) != programContext.DailyRecordList.Count - 1)
                {
                    record.OverNightChange = record.Open - programContext.DailyRecordList[programContext.DailyRecordList.IndexOf(record) + 1].Close;
                }
            }
        }

        public void CalcDailyChange(ProgramContext programContext)
        {
            programContext.DailyRecordList.OrderByDescending(x => x.Date);

            foreach (DailyStockRecord record in programContext.DailyRecordList)
            {
                if (programContext.DailyRecordList.IndexOf(record) != programContext.DailyRecordList.Count - 1)
                {
                    record.DailyChange = record.Close - record.Open;
                }
            }
        }

        public void CalcVolitilityRating(ProgramContext programContext)
        {

            
            List<DailyStockRecord> tempDailyRecordList = new List<DailyStockRecord>();

            var baseDailyRecordList = programContext.DailyRecordList.OrderBy(x => x.Date);

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

        public void CalcDividendYield(ProgramContext programContext)
        {
            var queryList = programContext.DailyRecordList.ToList();

            foreach (DailyStockRecord record in programContext.DailyRecordList)
            {
                var dividendYield = queryList.Where(x => x.Date > record.Date.AddYears(-1) && x.Date <= record.Date).Select(x => x.Dividend).Sum() / record.Close;
                dividendYield = dividendYield * 100;
                record.DividendYield = decimal.Round(dividendYield, 2, MidpointRounding.AwayFromZero);
            }
        }

        public void CalculationsForDailyRecord(ProgramContext programContext)
        {
            this.CalcDailyChange(programContext);
            this.CalcHigh52Week(programContext);
            this.CalcLow52Week(programContext);
            this.CalcOverNightChange(programContext);
            this.CalcVolitilityRating(programContext);
            this.CalcDividendYield(programContext);
        }
    }
}
