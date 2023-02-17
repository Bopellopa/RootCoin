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
            Blockchain rootcoin = new Blockchain();

            rootcoin.AddBlock(new Block(1, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 50", ""));
            rootcoin.AddBlock(new Block(2, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "amount: 200", ""));
            string blockJSON = JsonConvert.SerializeObject(rootcoin, Formatting.Indented);
            Console.WriteLine(blockJSON);
        }
    }

    class Blockchain
    {
        public List<Block> Chain { get; set; }
        public Blockchain()
        {
            this.Chain = new List<Block>();
            this.Chain.Add(CreateGenesisBlock());
        }
        public Block CreateGenesisBlock()
        {
            return new Block(0, DateTime.Now.ToString("yyyyMMddHHmmssffff"), "GENISIS BLOCK");
        }

        public Block GetLatestBlock()
        {
            return this.Chain.Last();
        }
        public void AddBlock(Block newBlock)
        {
            newBlock.PreviousHash = this.GetLatestBlock().Hash;
            newBlock.Hash = newBlock.CalculateHash();
            this.Chain.Add(newBlock);
        }
        public bool IsChainValid()
        {
            for ( int i = 1; i<this.Chain.Count; i++)
            {
                //check current hash = calc hash
                Block currentBlock = this.Chain[i];
                Block previousBlock = this.Chain[i - 1];
            }
        }
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
            this.Hash = CalculateHash();

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


