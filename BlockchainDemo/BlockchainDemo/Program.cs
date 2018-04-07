using Newtonsoft.Json;
using System;

namespace BlockchainDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain();
            blockchain.AddBlock(new Block(DateTime.Now, "{ sender: 'Henry', receiver: 'John', amount: 5}"));
            blockchain.AddBlock(new Block(DateTime.Now, "{ sender: 'Henry', receiver: 'John', amount: '10'}"));

            Console.WriteLine(JsonConvert.SerializeObject(blockchain, Formatting.Indented));

            Console.WriteLine($"Is blockchain valid: {blockchain.Validate()}");

            blockchain.Chain[1].Data = "{ sender: 'Henry', receiver: 'John', amount: 100}";

            Console.WriteLine($"Is blockchain still valid: {blockchain.Validate()}");

            blockchain.Chain[1].Hash = blockchain.Chain[1].CalculateHash();

            Console.WriteLine($"Is blockchain valid after fixed hash: {blockchain.Validate()}");

            Console.ReadKey();
        }
    }
}