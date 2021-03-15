using System;
using System.Collections.Generic;

namespace Archlab1.Task5
{
    public static class PrimeGenerator
    {
        public static IEnumerable<int> GeneratePrimes(int upto) {
            if (upto < 2) {
                yield break;
            }

            var stopAfter = (int) Math.Sqrt(upto);
            var sieve = new bool[upto + 1];
            Array.Fill(sieve, true);
            for (var i = 2; i < sieve.Length; i++) {
                if (sieve[i]) {
                    if (i <= stopAfter) {
                        for (var j = i * i; j < sieve.Length; j += i) {
                            sieve[j] = false;
                        }
                    }

                    yield return i;
                }
            }
        }
    }
}
