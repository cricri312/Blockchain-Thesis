using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
namespace InzynierkaBlockchain
{
    // NetInfo class, I have implemented this class only for get Ip information, for the issuerId in the blockchain
    class NetInfo
    {
        public string getIP(string InterfaceName)
        {
            string ipAddress = "";
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    if (ni.Name == InterfaceName)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                ipAddress = ip.Address.ToString();break;
                            }
                           
                        }
                        break;
                    }
                    
                }
                
            }
            return ipAddress;
        }
    }
}
