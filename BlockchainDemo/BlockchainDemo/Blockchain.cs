using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlockchainDemo
{
    public class Blockchain
    {
        IList<Block> _chain = new List<Block>();

        public IList<Block> Chain
        {
            get
            {
                return _chain;
            }
        }

        public Blockchain()
        {
            _chain.Add(CreateGenesisBlock());
        }

        public Block CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, "Genesis Block");
            block.Hash = block.CalculateHash();

            return block;
        }

        public void AddBlock(Block block)
        {
            if (_chain.Count > 0)
            {
                block.Index = _chain[_chain.Count - 1].Index + 1;
                block.PreviousHash = _chain[_chain.Count - 1].Hash;
                block.Hash = block.CalculateHash();
            }
            _chain.Add(block);
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
    }
}
