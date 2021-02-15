using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
namespace InzynierkaBlockchain
{
    //OnlineMode is simple Online demonstration of blockchain via UDP connection, all byte of data are visible,
    //I have used the 64base conversion, every byte of file is visible and not crypted
    class OnlineMode
    {
        public static Animation animation = new Animation();
        public static PeerReceiver PeerR = null;
        public static PeerSender PeerS = null;
        public static NetInfo netInfo = new NetInfo();
        public static Blockchain crisu = new Blockchain();
        public static string address = "None";
        public static void StartPeer()
        {
            PeerR = new PeerReceiver();
            PeerR.StartReceiver();
            
        }

        public void Main()
        {
            Thread thread1 = new Thread(StartPeer);
            Thread thread2 = new Thread(MainLoop);
            thread2.Start();
            thread1.Start();
            thread2.Join();
            thread1.Join();
        }
        public static void MainLoop()
        {
            Console.WriteLine("Online Mode chosen...");
            animation.SleepAndClear();
            int decision = 0; 
            while (decision != 6)
            {
                try
                {
                    animation.CompleteMenuOnlineOffline();
                    decision = int.Parse(Console.ReadLine());
                    switch (decision)
                    {
                        case 1:
                            Console.WriteLine(JsonConvert.SerializeObject(crisu, Formatting.Indented));
                            break;
                        case 2:
                            address = netInfo.getIP("Hamachi");
                            Console.WriteLine("---------------");
                            Console.WriteLine($"Current IP Address: {address}");
                            Console.WriteLine("---------------");
                            break;
                        case 3:
                            Console.Clear();
                            break;
                        case 4:
                            try
                            {
                                PeerS = new PeerSender();
                                Console.WriteLine("IP of receivier");
                                string receiver = Console.ReadLine();
                                Console.WriteLine("Give the path of the file:");
                                string path = Console.ReadLine();
                                byte[] file = File.ReadAllBytes(@path);
                                string filename = Path.GetFileName(path);
                                string tmp = Encoding.ASCII.GetString(file);
                                tmp = tmp + "\n" + "Filename--> " + "name:" + filename + ":name";
                                file = Encoding.ASCII.GetBytes(tmp);
                                string data = Convert.ToBase64String(file);
                                address = netInfo.getIP("Hamachi");
                                crisu.CreateT(new Transactions(address, receiver, data));
                                PeerS.sendInformation(JsonConvert.SerializeObject(crisu), receiver);
                                Console.Write("File sended, successfully ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("▄");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine("Given path is incorrect, retry..");
                                Console.WriteLine(ex.GetType().ToString());
                                Console.WriteLine(ex.Message.ToString());
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);

                                }
                                Console.Clear();
                            }

                            break;
                        case 5:
                            address = netInfo.getIP("Hamachi");
                  
                            for (int i = 1; i < crisu.Blocks.Count; i++)
                            {
                                if (crisu.Blocks[i].Transactions[0].RecipientId == address)
                                {
                                    
                                    byte[] outByte = Convert.FromBase64String(crisu.Blocks[i].Transactions[0].Data);
                                    String decoded = System.Text.Encoding.UTF8.GetString(outByte);
                                    int pFrom = decoded.IndexOf("name:") + "name:".Length;
                                    int pTo = decoded.LastIndexOf(":name");

                                    String result = decoded.Substring(pFrom, pTo - pFrom);
                                    Console.WriteLine("Downloaded file:::::" + result);
                                    File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + @"\" + result, outByte);

                                }
                            }

                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.GetType().ToString());
                    Console.WriteLine(ex.Message.ToString());
                    Console.WriteLine("Retry");
                }
            }
        }
    }
}
