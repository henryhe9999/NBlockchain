using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockchainDemo
{
    public class Block
    {
        int _index;
        DateTime _timeStamp;
        string _data;
        string _previousHash;
        string _hash;
        int _nonce = 0;

        public int Index
        {
            get
            {
                return _index;
            }
        }

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

        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public DateTime Timstamp
        {
            get
            {
                return _timeStamp;
            }
        }

        public Block(int index, DateTime timeStamp, string data)
        {
            _index = 0;
            _timeStamp = timeStamp;
            _data = data;
            _previousHash = null;
            _hash = null;
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{_index}-{_timeStamp}-{_data}-{_previousHash??""}-{_nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        public void Mine(int difficulty)
        {
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != new string('0', difficulty))
            {
                this._nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
