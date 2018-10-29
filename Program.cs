using System;
using Newtonsoft.Json;
using p2p;

namespace blockchain
{
    class Program
    {
        public static int Port = 5200;
        public static P2PServer Server = null;
        public static P2PClient Client = new P2PClient();
        public static Blockchain PhillyCoin = new Blockchain();
        public static string name = "Unkown";

        static void Main(string[] args)
        {
            try
            {
                PhillyCoin.InitializeChain();

                if (args.Length >= 1)
                    Port = int.Parse(args[0]);
                if (args.Length >= 2)
                    name = args[1];

                if (Port > 0)
                {
                    Console.Write("IP Server: ");
                    string ipServer = Console.ReadLine();
                    Server = new P2PServer();
                    Server.Start(ipServer);
                }
                if (name != "Unkown")
                {
                    Console.WriteLine($"Current user is {name}");
                }

                Console.WriteLine("=========================");
                Console.WriteLine("1. Connect to a server");
                Console.WriteLine("2. Add a transaction");
                Console.WriteLine("3. Display Blockchain");
                Console.WriteLine("4. Exit");
                Console.WriteLine("=========================");

                int selection = 0;
                while (selection != 4)
                {
                    switch (selection)
                    {
                        case 1:
                            Console.WriteLine("Please enter the server URL");
                            string serverURL = Console.ReadLine();
                            Client.Connect($"{serverURL}/Blockchain");
                            break;
                        case 2:
                            Console.WriteLine("Please enter the receiver name");
                            string receiverName = Console.ReadLine();
                            Console.WriteLine("Please enter the amount");
                            string amount = Console.ReadLine();
                            PhillyCoin.CreateTransaction(new Transaction(name, receiverName, int.Parse(amount)));
                            PhillyCoin.ProcessPendingTransactions(name);
                            Client.Broadcast(JsonConvert.SerializeObject(PhillyCoin));
                            break;
                        case 3:
                            Console.WriteLine("Blockchain");
                            Console.WriteLine(JsonConvert.SerializeObject(PhillyCoin, Formatting.Indented));
                            break;

                    }

                    Console.WriteLine("Please select an action");
                    string action = Console.ReadLine();
                    selection = int.Parse(action);
                }

                Client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void RunBlockchainTransaction()
        {
            var startTime = DateTime.Now;

            Blockchain phillyCoin = new Blockchain();
            phillyCoin.CreateTransaction(new Transaction("Henry", "MaHesh", 10));
            phillyCoin.ProcessPendingTransactions("Bill");
            //Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));  

            phillyCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 5));
            phillyCoin.CreateTransaction(new Transaction("MaHesh", "Henry", 5));
            phillyCoin.ProcessPendingTransactions("Bill");

            phillyCoin.CreateTransaction(new Transaction("João", "Henry", 5));
            phillyCoin.ProcessPendingTransactions("Bill");

            var endTime = DateTime.Now;

            Console.WriteLine($"Duration: {endTime - startTime}");

            Console.WriteLine("=========================");
            Console.WriteLine($"Henry' balance: {phillyCoin.GetBalance("Henry")}");
            Console.WriteLine($"MaHesh' balance: {phillyCoin.GetBalance("MaHesh")}");
            Console.WriteLine($"Bill' balance: {phillyCoin.GetBalance("Bill")}");

            Console.WriteLine("=========================");
            Console.WriteLine($"phillyCoin");
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
        }

        static void RunBlockchain()
        {
            var startTime = DateTime.Now;

            Blockchain phillyCoin = new Blockchain();
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));

            var endTime = DateTime.Now;

            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            string blockchainValida = phillyCoin.IsValid() ? "Sim" : "Não";
            Console.WriteLine("Blockchain válida? {0}", blockchainValida);

            Console.WriteLine($"\nDuração: {endTime - startTime}\n");

            Console.WriteLine("\nAlteração de dados");
            phillyCoin.Chain[1].Data = "{{sender:Zeus,receiver:Poseidon,amount:200}}";
            blockchainValida = phillyCoin.IsValid() ? "Sim" : "Não";
            Console.WriteLine("Blockchain válida? {0}", blockchainValida);
        }

    }
}
