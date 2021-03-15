using System;

namespace Archlab1.Task2
{
    public static class YearPrinter
    {
        public static void PrintYears(int from, int to) {
            for (var year = from; year <= to; year++) {
                Console.Write(year);
                Console.WriteLine((year % 100 != 0 || year % 400 == 0) && year % 4 == 0
                                      ? " високосный"
                                      : " не високосный");
            }
        }
    }
}
