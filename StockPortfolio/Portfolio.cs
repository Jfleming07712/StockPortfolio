using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class Portfolio
    {
        public int PortfolioId { get; set; }
        public string PortfolioName { get; set; }
        public decimal PlYtd{ get; set; }
        public decimal PlDaily { get; set; }
        public decimal StockWorth { get; set; }
        public decimal CashBalance { get; set; }
        public decimal PortfolioWorth { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
