using System.Numerics;

namespace Archlab1.Task4
{
    public class FactorialCalculator
    {
        public static BigInteger Factorial(BigInteger number) {
            BigInteger res = 1;
            for (ulong i = 1; i <= number; i++) {
                res *= i;
            }

            return res;
        }
    }
}
