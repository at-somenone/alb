using System;
using System.Collections.Generic;
using System.Numerics;
using Archlab1.Task1;
using Archlab1.Task2;
using Archlab1.Task3;
using Archlab1.Task4;
using Archlab1.Task5;

// todo go over everything one more time and fix it
namespace Archlab1
{
    class Program
    {
        public static void Main(string[] args) {
            var exit = false;
            while (!exit) {
                Console.Clear();
                Console.WriteLine(@"1. Command line args
2. Years 1900-2000
3. Fibonacci
4. Factorial
5. Primes
0. Quit");

                var selection = Console.ReadKey().KeyChar;
                Console.Clear();
                switch (selection) {
                    case '0':
                        exit = true;
                        break;
                    case '1':
                        Task1(args);
                        break;
                    case '2':
                        Task2();
                        break;
                    case '3':
                        Task3();
                        break;
                    case '4':
                        Task4();
                        break;
                    case '5':
                        Task5();
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        break;
                }

                Console.ReadKey(true);
            }
        }

        private static void Task1(IEnumerable<string> args) {
            EnumerablePrinter.PrintEnumerable(args);
        }


        private static void Task2() {
            YearPrinter.PrintYears(1900, 2000);
        }

        private static void Task3() {
            Console.Write("Up to: ");
            if (int.TryParse(Console.ReadLine(), out var upto)) {
                foreach (var number in FibonacciGenerator.GenerateFibonacci(upto))
                    Console.WriteLine(number);
            }
            else
                Console.WriteLine("Invalid input");
        }

        private static void Task4() {
            Console.Write("Of: ");
            if (BigInteger.TryParse(Console.ReadLine(), out var of) && of >= 0)
                Console.WriteLine(FactorialCalculator.Factorial(of));
            else
                Console.WriteLine("Invalid input");
        }

        private static void Task5() {
            Console.Write("Up to: ");
            if (int.TryParse(Console.ReadLine(), out var upto)) {
                foreach (var number in PrimeGenerator.GeneratePrimes(upto))
                    Console.WriteLine(number);
            }
            else
                Console.WriteLine("Invalid input");
        }
    }
}
