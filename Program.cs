using System;
using Newtonsoft.Json;

namespace blockchain
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain phillyCoin = new Blockchain();  
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));  
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));  
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));  
            
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            string blockchainValida = phillyCoin.IsValid() ? "Sim" : "Não";
            Console.WriteLine("Blockchain válida? {0}", blockchainValida);

            Console.WriteLine("\nAlteração de dados");
            phillyCoin.Chain[1].Data = "{{sender:Zeus,receiver:Poseidon,amount:200}}";
            blockchainValida = phillyCoin.IsValid() ? "Sim" : "Não";
            Console.WriteLine("Blockchain válida? {0}", blockchainValida);
        }
    }
}
