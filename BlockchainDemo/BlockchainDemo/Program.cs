using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlockchainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain heCoin = new Blockchain();
            heCoin.CreateTransaction(new Transaction("Henry", "John", 50));
            heCoin.ProcessPendingTransactions("Andy");

            heCoin.CreateTransaction(new Transaction("Henry", "John", 100));
            heCoin.ProcessPendingTransactions("Brian");

            heCoin.ProcessPendingTransactions("Carly");
 
            Console.WriteLine(JsonConvert.SerializeObject(heCoin, Formatting.Indented));

            Console.WriteLine($"Henry's balance: {heCoin.GetBalance("Henry")}");
            Console.WriteLine($"John's balance: {heCoin.GetBalance("John")}");
            Console.WriteLine($"Andy's balance: {heCoin.GetBalance("Andy")}");
            Console.WriteLine($"Brian's balance: {heCoin.GetBalance("Brian")}");
            Console.WriteLine($"Carly's balance: {heCoin.GetBalance("Carly")}");

            Console.WriteLine($"Is blockchain valid: {heCoin.Validate()}");

            var fakeTransaction = new List<Transaction>();
            fakeTransaction.Add(new Transaction("Henry", "John", 1000));
            heCoin.Chain[1].Transactions = fakeTransaction;

            Console.WriteLine($"Is blockchain still valid: {heCoin.Validate()}");

            heCoin.Chain[1].Hash = heCoin.Chain[1].CalculateHash();

            Console.WriteLine($"Is blockchain valid after fixed hash: {heCoin.Validate()}");

            Console.ReadKey();
        }
    }
}