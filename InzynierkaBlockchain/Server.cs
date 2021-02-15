using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
namespace InzynierkaBlockchain
{
    //Server side also i have used WebSockerSharp library
    public class Server: WebSocketBehavior
    {
        WebSocketServer server = null;
        Boolean syncronized = false;
        Animation animation = new Animation();
        //this function initialize the server on a specific port
        public  void Listen()
        {
            server = new WebSocketServer($"ws://127.0.0.1:{OfflineMode.Port}");
            server.AddWebSocketService<Server>("/Chain");
            server.Start();
            Console.WriteLine($"Server started on ws://127.0.0.1:{OfflineMode.Port}");
        }
        public void Stop()
        {
            server.Stop();
        }
        //The server listen, if receive a message and check it
        protected override void OnMessage(MessageEventArgs e)
        {
            //e.Data is the message received from other client, if lenght is > 20, i have used 20 because when the client send 
            // initial message, have lenght 20, because of this if is initial message the server ignore it
            if (e.Data.Length >= 20)
            {
                Blockchain newBlocks = JsonConvert.DeserializeObject<Blockchain>(e.Data);
                newBlocks.Blocks.RemoveAt(0);
                
                if(newBlocks.checkBool()&& newBlocks.Blocks.Count>OfflineMode.crisu.Blocks.Count)
                {
                    List<Transactions> newT = new List<Transactions>();
                    newT.AddRange(newBlocks.Transactions);
                    newT.AddRange(OfflineMode.crisu.Transactions);
                    newBlocks.Transactions = newT;
                    OfflineMode.crisu = newBlocks;
                    
                }
            }
            else
            {
                Console.WriteLine(e.Data);
            }
            if (!syncronized)
            {
                Send(JsonConvert.SerializeObject(OfflineMode.crisu));
                syncronized = true;
            }
        }
    }
}
