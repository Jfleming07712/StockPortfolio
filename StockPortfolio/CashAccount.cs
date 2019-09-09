using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class CashAccount
    {
        public int AccountId { get; set; }
        public decimal CashPreviousBalance { get; set; }
        public decimal CashAddWithdraw { get; set; }
        public decimal CashBalance { get; set; }

    }
}
