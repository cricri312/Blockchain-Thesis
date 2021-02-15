using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
namespace InzynierkaBlockchain
{
    //This the receiver of UDP messge, Ihave used thread to implement the reciver and sender in one class
    class PeerReceiver
    {

        private const int port = 11000;
        public void StartReceiver()
        {
            
            UdpClient listner = new UdpClient(port);
            IPEndPoint iP = new IPEndPoint(IPAddress.Any, port);
            while (true)
            {
                byte[] bytes = listner.Receive(ref iP);
                Blockchain newBlocks = JsonConvert.DeserializeObject<Blockchain>(Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                newBlocks.Blocks.RemoveAt(0);
                    if (newBlocks.checkBool() && newBlocks.Blocks.Count > OnlineMode.crisu.Blocks.Count)
                    {
                        List<Transactions> newT = new List<Transactions>();
                        newT.AddRange(newBlocks.Transactions);
                        newT.AddRange(OnlineMode.crisu.Transactions);
                        newBlocks.Transactions = newT;
                        OnlineMode.crisu = newBlocks;
                   
                }
                else
                {
                    Console.WriteLine("ERROR");
                }

            }
        }
        public void StopReceiver()
        {
            UdpClient listner = new UdpClient(port);
            listner.Close();
        }

    }
}
