//CRISTIAN BOFFA 156262
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace InzynierkaBlockchain
{
    public class Block
    {
        //set parameters get set to all variable needed in blockchain
        public int Index { get; set; } //Index of a single block    
        public DateTime TimeStp { get; set; } //Is a time to make unique a block
        public string PrevHash { get; set; } //The hash of previous block
        public string Hash { get; set; } //Current hash
        public IList<Transactions> Transactions { get; set; }
        public int Nonce { get; set; } //Nonce a value added to the end of hash to increase the security of hash
      

        //the constructor take 3 variables as argument, because time is variable
        // prevHash is a hash o previous Block, Inforrmation is different on any block
        public Block(DateTime timeStp,string prevHash, IList<Transactions> transactions)
        {
            Nonce = CalculateNonce(); ;
            Index = 0; //set the index to 0, genesis block
            TimeStp = timeStp ; 
            PrevHash = prevHash;
            Transactions=transactions;
            Hash = Hash_();
        }
        //Emulated Nonce value, Nonce value is calculated in different ways, but for this version is randomized
        public int CalculateNonce()
        {
            Random rnd = new Random();
            return rnd.Next(0, 10000);
        }
        public string Hash_()
        {   //Hash function using the SHA256 algorithm
            //calculating the Hash of the block, we need to 
            SHA256 sha256 = SHA256.Create();
            byte[] input = Encoding.ASCII.GetBytes($"{TimeStp}-{PrevHash??""}-{JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] output = sha256.ComputeHash(input);
            return Convert.ToBase64String(output);
        }
        
    }
}
