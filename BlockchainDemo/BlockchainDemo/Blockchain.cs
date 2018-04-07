using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainDemo
{
    public class Blockchain
    {
        IList<Transaction> _pendingTransactions = new List<Transaction>();
        IList<Block> _chain = new List<Block>();
        int _difficulty = 2;
        int _miningReward = 1;

        public IList<Block> Chain
        {
            get
            {
                return _chain;
            }
        }

        public Blockchain()
        {
            CreateGenesisBlock();
        }

        public void CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, _pendingTransactions);
            block.Mine(_difficulty);
            _chain.Add(block);
            _pendingTransactions = new List<Transaction>();
        }

        public void ProcessPendingTransactions(string minerAddress)
        {
            Block block = new Block(DateTime.Now, _pendingTransactions);
            block.PreviousHash = _chain[_chain.Count - 1].Hash;
            block.Mine(_difficulty);
            _chain.Add(block);

            _pendingTransactions = new List<Transaction>();
            CreateTransaction(new Transaction(null, minerAddress, _miningReward));
        }

        public bool Validate()
        {
            for (int i = 1; i < _chain.Count; i++)
            {
                Block currentBlock = _chain[i];
                Block previousBlock = _chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }

            return true;
        }

        public void CreateTransaction(Transaction transaction)
        {
            _pendingTransactions.Add(transaction);
        }

        public int GetBalance(string address)
        {
            int balance = 0;

            for (int i = 0; i < _chain.Count; i++)
            {
                for (int j = 0; j < _chain[i].Transactions.Count; j++)
                {
                    var transaction = _chain[i].Transactions[j];

                    if (transaction.FromAddress == address)
                    {
                        balance -= transaction.Amount;
                    }

                    if (transaction.ToAddress == address)
                    {
                        balance += transaction.Amount;
                    }
                }
            }

            return balance;
        }
    }
}
