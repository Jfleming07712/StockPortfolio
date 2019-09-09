using System;
using System.Collections.Generic;
using System.Text;

namespace StockPortfolio
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public CashAccount CashAccount { get; set; }
        public Portfolio Portfolio { get; set; }

    }
}
