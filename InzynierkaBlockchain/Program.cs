//CRISTIAN BOFFA 156262
using System;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
namespace InzynierkaBlockchain
{
    //The main Program, Program.cs is short
    //because all is implemented in class
    //44 lines
    //CRISTIAN BOFFA 
    class Program
    {
        public static OfflineMode offmode = new OfflineMode();
        public static OnlineMode onmode = new OnlineMode();
        public static Animation animation = new Animation();
        static void Main(string[] args)
        {
            animation.Init();
            int nummer;
            string decision = "0";
            while (int.Parse(decision) != 3)
            {
                decision = Console.ReadLine();
                if (!int.TryParse(decision, out nummer))
                {
                    Console.WriteLine("Retry, or exit");
                    decision = "0";
                }
                switch (int.Parse(decision))
                {
                    case 1:
                        offmode.MainLoopFirst();
                        break;
                    case 2:
                        onmode.Main();
                        break;
                }
            }
        }
    }
}