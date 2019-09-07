using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StockPortfolio
{
    public class FileDownloader
    {
        public void DownloadFile(string symbol)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://query1.finance.yahoo.com/v7/finance/download/"+ symbol +"?period1=1536276044&period2=1567812044&interval=1d&events=history&crumb=PQJtjyeBuE", "TestDownload");
            }
        }
    }
}
