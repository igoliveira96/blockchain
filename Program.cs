using System;
using Newtonsoft.Json;

namespace blockchain
{
    class Program
    {
        static void Main(string[] args)
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
