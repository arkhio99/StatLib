using System;
using System.Collections.Generic;
using System.Text;

namespace StatLib
{
    static class Combinatoricks
    {
        public static int Factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * Factorial(x - 1);
            }
        }

        public static int C(int k, int n)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }
    }
}
