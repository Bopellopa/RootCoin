using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace RootCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Block newblock = new Block(1, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 50", "");
            string blockJSON = JsonConvert.SerializeObject(newblock, Formatting.Indented);
            Console.WriteLine(blockJSON);
        }
    }

    class Blockchain
    {

    }

    class Block
    {
        public int Index { get; set; }
        public string PreviousHash { get; set; }
        public string Timestamp { get; set; }
        public string Data { get; set; }
        public string Hash { get; set; }

        public Block(int index, string timestamp, string data, string previousHash = "")
        {
            this.Index = index;
            this.Timestamp = timestamp;
            this.Data = data;
            this.PreviousHash = previousHash;

        }

        public string CalculateHash()
        {
            string blockData = this.Index + this.PreviousHash + this.Timestamp + this.Data;
            byte[] blockBytes = Encoding.ASCII.GetBytes(blockData);
            byte[] hashBytes = SHA256.Create().ComputeHash(blockBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }
    }
}


