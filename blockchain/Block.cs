using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace blockchain
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public string Data { get; set; }
        public int Nonce { get; set; } = 0;
        public IList<Transaction> Transactions { get; set; }

        public Block(DateTime timeStamp, string previousHash, string data)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Data = data;
            Hash = CalculateHash();
        }

        public Block(DateTime timeStamp, string previousHash, IList<Transaction> transactions)
        {
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Transactions = transactions;
        }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");  
            byte[] outputBytes = sha256.ComputeHash(inputBytes);  
  
            return Convert.ToBase64String(outputBytes);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);  
            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeros)  
            {  
                this.Nonce++;  
                this.Hash = this.CalculateHash();
            }
        }

    }
}