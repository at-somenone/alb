using System;

namespace ArchlabEightHundred.CalcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var calc = new ArchlabEightHundred.Com.Calculator();
            Console.WriteLine("Введите выражение: ");
            var exp = Console.ReadLine();
            try
            {
                Console.WriteLine(calc.Calculate(exp));
            }
            catch
            {
                Console.WriteLine("Не удалось вычислить результат");
            }
            Console.ReadKey();
        }
    }
}
