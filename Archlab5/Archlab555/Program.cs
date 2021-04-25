using System;
using Archlab555.net.yandex.speller;

namespace Archlab555
{
    class Program
    {
        private static void Main(string[] args) {
            var hhhh = new SpellService();
            Console.Write("Введите строку, которую нужно проверить: ");
            var str = Console.ReadLine().Replace("\n", "");
            var yeahhh = hhhh.checkText(str, "ru", 1, "");
            var lastIndex = 0;
            ;
            foreach (var idk in yeahhh) {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(str.Substring(lastIndex, idk.col - lastIndex));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(str.Substring(idk.col, idk.len));
                lastIndex = idk.col + idk.len;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(str.Substring(lastIndex));
        }
    }
}
