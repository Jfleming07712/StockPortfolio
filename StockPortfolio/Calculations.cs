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

            foreach(DailyStockRecord record in dailyRecordList)
            {
                record.High52Week = queryList.Where(x => x.Date > record.Date.AddYears(-1) && x.Date <= record.Date).Select(x => x.High).Max();
                
            }
        }
    }
}
