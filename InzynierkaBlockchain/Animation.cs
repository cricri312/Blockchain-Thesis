using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
namespace InzynierkaBlockchain
{
    //This class is only for animation, like menu
    class Animation : IAnimation
    {
        public void Init()
        {
            Console.Clear();
            Console.WriteLine("--------------------");
            Console.WriteLine("CrisuChain");
            Console.WriteLine("--------------------");
            string text = "Hello, You have two method of testig this app:\n" +
                "1. Offilne Mode, exchange file, test blockchain only on your network\n"+
                "2. Online Mode, exchange file beetween other peers via VPN tunnel\n"+
                "3. Exit\n"+
                "Select your decision and start the adventure\n";
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(7);
            }
        }
        public void SleepAndClear()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.Clear();
        }

        public void CompleteMenuOffilne()
        {
            Console.WriteLine("1. Display Blockchain");
            Console.WriteLine("2. Display Information");
            Console.WriteLine("3. Clear Display");
            Console.WriteLine("4. Connect to server");
            Console.WriteLine("5. New Transaction");
            Console.WriteLine("6. Check Blockchain and download file");
            Console.WriteLine("7. Exit");
        }

        public void CompleteMenuOnlineOffline()
        {
            Console.WriteLine("1. Display Blockchain");
            Console.WriteLine("2. Display Information");
            Console.WriteLine("3. Clear Display");
            Console.WriteLine("4. New Transaction");
            Console.WriteLine("5. Check Blockchain and download file");
            Console.WriteLine("6. Exit");
        }
 

    }
}
