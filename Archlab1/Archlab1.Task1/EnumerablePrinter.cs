using System;
using System.Collections.Generic;

namespace Archlab1.Task1
{
    public static class EnumerablePrinter
    {
        public static void PrintEnumerable(IEnumerable<object> ienum) {
            foreach (var element in ienum) {
                Console.WriteLine(element);
            }
        }
    }
}
