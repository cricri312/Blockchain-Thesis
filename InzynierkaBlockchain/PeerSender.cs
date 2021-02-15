using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace InzynierkaBlockchain
{
    class PeerSender
    {
        //This calss using Socket to send information to other UDP client, obviously only in the VPN tunnelling
        public void sendInformation(string data, string ipAddress)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Console.WriteLine(ipAddress);
            IPAddress broadcast = IPAddress.Parse(ipAddress);
            byte[] sendbuf = Encoding.ASCII.GetBytes(data);
            
            IPEndPoint ep = new IPEndPoint(broadcast, 11000);

            s.SendTo(sendbuf, ep);

            Console.WriteLine("Message sent to the broadcast address");
        }
    }
}
