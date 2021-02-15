using System;
using System.Collections.Generic;
using System.Text;

namespace InzynierkaBlockchain
{
    public class Transactions
    {
        public string IssuerId { get; set; } //the address who sent data
        public string RecipientId { get; set; } //the adress who receive data
        public string Data { get; set; } // //some data
        public Transactions(string issuerID, string recipientID, string data)
        {
            Data = data;
            IssuerId = issuerID;
            RecipientId = recipientID;
        }

    }
}
