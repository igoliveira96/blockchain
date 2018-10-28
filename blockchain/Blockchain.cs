
using System;
using System.Collections.Generic;

namespace blockchain
{
    public class Blockchain
    {
        public IList<Block> Chain { get; set; }

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        private void InitializeChain()
        {
            Chain = new List<Block>();
        }

        private Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, "{}");
        }

        private void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        private Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            Chain.Add(block);
        }

        // Verificar se os blocos da blockchain não foram adulterados
        public bool IsValid()  
        {  
            for (int i = 1; i < Chain.Count; i++)  
            {  
                Block currentBlock = Chain[i];  
                Block previousBlock = Chain[i - 1];  
        
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