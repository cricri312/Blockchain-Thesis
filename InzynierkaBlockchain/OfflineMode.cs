using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.IO;
using System.Drawing;
namespace InzynierkaBlockchain
{
    //I have divided my 2 program modes and it is the offline mode where you can understand the blockchain mechanism
    class OfflineMode

    {
        public static Animation animation = new Animation();
        public static int Port = 1;
        public static string address = "None";
        public static Server Server = null;
        public static Client Client = new Client();
        public static Blockchain crisu = new Blockchain();
        
        //The main loop is called MainLoop First because once the user has established a connection,
        //switches to safe mode
        public void MainLoopFirst()
        {
            Console.WriteLine("Offline Mode chosen...");

            for (; ; )
            {
                Console.WriteLine("Give a valid port number");
                int number;
                string tmp = Console.ReadLine();
                if (int.TryParse(tmp, out number))
                {
                    Port = int.Parse(tmp);
                    if (Port > 0 && Port < 99999)
                    {
                        break;
                    }
                    else { Console.WriteLine("Retry"); }
                }
            }
            for (; ; )
            {
                Console.WriteLine("Give your name");
                address = Console.ReadLine();
                if (address.Length != 0) break;
            }
            if (Port > 0)
            {
                Server = new Server();
                Server.Listen();
            }
            animation.SleepAndClear();

            int decision = 0;
          
            while (decision != 7)
            {
                try
                {
                    animation.CompleteMenuOffilne();
                    decision = int.Parse(Console.ReadLine());
                    switch (decision)
                    {
                        case 1:
                            Console.WriteLine(JsonConvert.SerializeObject(crisu, Formatting.Indented));
                            break;
                        case 2:
                            Console.WriteLine("---------------");
                            Console.WriteLine($"Server start on address: ws://127.0.0.1:{Port}");
                            Console.WriteLine($"Name of current address: {address}");
                            Console.WriteLine("---------------");
                            break;
                        case 3:
                            Console.Clear();
                            break;
                        case 4:
                            //I used try catch block for security reason, because if the user type wrong url, the program didn't crash
                            try
                            {
                                Console.WriteLine("Url address");
                                string url = Console.ReadLine();
                                Client.ConnectToABlockchain($"{url}/Chain");
                                //Switch to secure mode
                                MainLoopSecure();
                            }
                            catch (Exception)
                            {

                                Console.WriteLine("Given address is invalid, retry");
                            }

                            break;
                        case 5:
                            try
                            {
                                Console.WriteLine("Name of receiver");
                                string receiver = Console.ReadLine();
                                Console.WriteLine("Give the path of the file:");
                                string path = Console.ReadLine();
                                byte[] file = File.ReadAllBytes(@path);
                                string filename = Path.GetFileName(path);
                                string tmp = Encoding.ASCII.GetString(file);
                                tmp = tmp + "\n" + "Filename--> " + "name:" + filename + ":name";
                                file = Encoding.ASCII.GetBytes(tmp);
                                string data = Convert.ToBase64String(file);
                                crisu.CreateT(new Transactions(address, receiver, data));
                                Client.Send(JsonConvert.SerializeObject(crisu));
                                Console.Write("File sended, successfully ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("▄");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            catch (Exception)
                            {

                                Console.WriteLine("Given path is incorrect, retry..");
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);

                                }
                                Console.Clear();
                            }

                            break;
                        case 6:
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
                        case 7:
                            Environment.Exit(0);
                            break;
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine("Retry");
                }
            }
        }
        //The secure loop
        public void MainLoopSecure()
        {

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
                            Console.WriteLine("---------------");
                            Console.WriteLine($"Server start on address: ws://127.0.0.1:{Port}");
                            Console.WriteLine($"Name of current address: {address}");
                            Console.WriteLine("---------------");
                            break;
                        case 3:
                            Console.Clear();
                            break;
                        case 4:
                            try
                            {
                                Console.WriteLine("Name of receiver");
                                string receiver = Console.ReadLine();
                                Console.WriteLine("Give the path of the file:");
                                string path = Console.ReadLine();
                                byte[] file = File.ReadAllBytes(@path);
                                string filename = Path.GetFileName(path);
                                string tmp = Encoding.ASCII.GetString(file);
                                tmp = tmp + "\n" + "Filename--> " + "name:" + filename + ":name";
                                file = Encoding.ASCII.GetBytes(tmp);
                                string data = Convert.ToBase64String(file);
                                crisu.CreateT(new Transactions(address, receiver, data));
                                Client.Send(JsonConvert.SerializeObject(crisu));
                                Console.Write("File sended, successfully ");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("▄");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                            catch (Exception)
                            {

                                Console.WriteLine("Given path is incorrect, retry..");
                                for (int i = 0; i < 4; i++)
                                {
                                    Console.Write(".");
                                    Thread.Sleep(500);

                                }
                                Console.Clear();
                            }

                            break;
                        case 5:
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
                            Server.Stop();
                            Environment.Exit(0);
                            break;
                    }

                }
                catch (Exception)
                {

                    Console.WriteLine("Retry");
                }
            }
        }
    }
}
