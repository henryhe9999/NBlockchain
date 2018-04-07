using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainDemo
{
    public class Block
    {
        DateTime _timeStamp;
        IList<Transaction> _transactions;
        string _previousHash;
        string _hash;
        int _nonce = 0;

        public string PreviousHash
        {
            get
            {
                return _previousHash;
            }
            set
            {
                _previousHash = value;
            }
        }
        public string Hash
        {
            get
            {
                return _hash;
            }
            set
            {
                _hash = value;
            }
        }

        public IList<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }
            set
            {
                _transactions = value;
            }
        }

        public DateTime Timstamp
        {
            get
            {
                return _timeStamp;
            }
        }

        public Block(DateTime timeStamp, IList<Transaction> transactions)
        {
            _timeStamp = timeStamp;
            _transactions = transactions;
            _previousHash = null;
            _hash = null;
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{_timeStamp}-{JsonConvert.SerializeObject(_transactions)}-{_previousHash??""}-{_nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)
            {
                this._nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
