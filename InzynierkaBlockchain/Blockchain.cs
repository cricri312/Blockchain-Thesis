//CRISTIAN BOFFA 156262
using System;
using System.Collections.Generic;
using System.Text;

namespace InzynierkaBlockchain
{
    public class Blockchain
    {
        //I use indexed list, because is already indexed
        public IList<Transactions> Transactions = new List<Transactions>();
        public IList<Block> Blocks { get; set; }
        //In the constructor I declare 2 function, that must have to be
        public Blockchain() { Init_Block(); AddGenBlock(); }  
        //Init block to initiante a new list of blok
        public void Init_Block()
        {
            Blocks = new List<Block>();
        }
        //Genesis block i a 0 block that is necessary, but don't have prevHash
        //prevhash of null is null
        public Block Genesis_Block()
        {
            Block block = new Block(DateTime.Now, null, Transactions);
            Transactions = new List<Transactions>();
            return block;
        }
        //This simple, function only add genesis block into indexed list
        public void AddGenBlock()
        {
            Blocks.Add(Genesis_Block());
        }
        //this function is necessary, because return the latest block created
        //example: I Have created a block 1 so the latest is the block 0
        //so the prev hash of the my block 1 is the hash of the block 0.
        public Block LastBlock()
        {
            return Blocks[Blocks.Count - 1];
        }
        //create a transaction and added to our blockchain
        public void CreateT(Transactions transaction)
        {
            Transactions.Add(transaction);
            Block block = new Block(DateTime.Now, LastBlock().Hash, Transactions);
            AddBlock(block);
            Transactions = new List<Transactions>();
        }
        //the main function that add block to the Indexed list of blocks
        //take as argument a Object Block 
        public void AddBlock(Block block)
        {
            Block lastBlock = LastBlock();
            block.Index = lastBlock.Index + 1;
            block.PrevHash = lastBlock.Hash;
            block.Hash = block.Hash_();
            Blocks.Add(block);
        }
        //function that returns a list, check if blocks is corrupted
        //based of hash and prevHash chcecking
        public List<int> CheckBlocks()
        {
            List<int> exception = new List<int>();
            for (int i = 1; i < Blocks.Count; i++)
            {
                Block prev = Blocks[i - 1];
                Block current = Blocks[i];
                if (current.PrevHash != prev.Hash) exception.Add(i);
                else
                if (current.Hash != current.Hash_()) exception.Add(i);
            }
            return exception;
        }
        //Only print if blocks are manomitted or not
        public void PrintException(List<int> exp)
        {
            if (exp.Count==0)
            {
                Console.WriteLine("No manomission");
            }
            else
            {
                Console.WriteLine("Manomitted Block");
            }
        }
        //chceckBool is used in the clien and server side to determinate if block is not manomitted
        //It's easy to use because return Boolean value 0 or 1
        public Boolean checkBool()
        {
            for (int i = 1; i < Blocks.Count; i++)
            {
                Block currentBlock = Blocks[i];
                Block previousBlock = Blocks[i - 1];

                if (currentBlock.Hash != currentBlock.Hash_())
                {
                    return false;
                }

                if (currentBlock.PrevHash != previousBlock.Hash)
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}
