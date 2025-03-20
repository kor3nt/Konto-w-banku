using Bank;

namespace BankConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Konto molenda = new Konto("Molenda", 100);
            Console.WriteLine($"{molenda}\n");

            molenda = molenda.ZmianaNaPlus();
            Console.WriteLine($"{molenda}\n");

            molenda.Wyplata(100);
            Console.WriteLine($"{molenda}\n");

            molenda.Wplata(2000);
            Console.WriteLine($"{molenda}\n");

            molenda.ZmianaKonta();
            Console.WriteLine($"{molenda}\n");

            molenda.BlokujKonto();
            Console.WriteLine($"{molenda}\n");

            molenda.OdblokujKonto();
            Console.WriteLine($"{molenda}\n");
        }
    }
}