using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainDemo
{
    public class Transaction
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Amount { get; set; }
    }
}
