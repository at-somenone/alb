using System.Collections.Generic;

namespace Archlab1.Task3
{
    public static class FibonacciGenerator
    {
        public static IEnumerable<int> GenerateFibonacci(int upto) {
            var a = 0;
            var b = 1;

            if (upto > a) yield return a;

            while (b <= upto) {
                yield return b;

                var t = a;
                a = b;
                b += t;
            }
        }
    }
}
