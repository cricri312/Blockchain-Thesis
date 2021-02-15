using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using Newtonsoft.Json;
namespace InzynierkaBlockchain
{
    class Client
    {
        //For the client side i have used the WebSockerSharp library, https://github.com/sta/websocket-sharp#websocket-client 
        IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();

        //this function connect emulated a connection to a blockchain, via address url.
        public void ConnectToABlockchain(string address_url)
        {
            if (!wsDict.ContainsKey(address_url))
            {
                WebSocket client = new WebSocket(address_url);
                client.OnMessage += Client_OnMessage;
                client.Connect();
                client.Send("Hi i'm connected");
                wsDict.Add(address_url, client);
            }
        }
        private void Client_OnMessage(object sender, MessageEventArgs e)
        {
            
            Blockchain newBlocks = JsonConvert.DeserializeObject<Blockchain>(e.Data);
            newBlocks.Blocks.RemoveAt(0);
            if (newBlocks.checkBool() && newBlocks.Blocks.Count > OfflineMode.crisu.Blocks.Count)
            {
                List<Transactions> newT = new List<Transactions>();
                newT.AddRange(newBlocks.Transactions);
                newT.AddRange(OfflineMode.crisu.Transactions);
                newBlocks.Transactions = newT;
                OfflineMode.crisu = newBlocks;
            }
        }

        //this method is designed not only for one server but in the future can be deployed in a more comlex blockchain structure
        public void Send(string data)
        {
            foreach (var item in wsDict)
            {
                item.Value.Send(data);
            }

        }

    }
}
