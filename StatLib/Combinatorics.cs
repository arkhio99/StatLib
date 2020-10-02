using System;
using System.Collections.Generic;
using System.Text;

namespace StatLib
{
    /// <summary>
    /// Библиотека, содержащая некоторые функции комбинаторики
    /// </summary>
    public static class Combinatorics
    {
        /// <summary>
        /// Вычисляет факториал числа. (Не использовать при x > 12)
        /// </summary>
        /// <param name="x">Число</param>
        /// <returns>Факториал числа.</returns>
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

        /// <summary>
        /// Вычисляет количество сочетаний без повторений из n элементов по k. (Не использовать при n > 24, вылетит ошибка)
        /// </summary>
        /// <param name="k">Мощность сочетания.</param>
        /// <param name="n">Мощность множества.</param>
        /// <returns>Количество сочетаний без повторений.</returns>
        public static int C(int k, int n)
        {
            int result = 1;
            
            if (k > n - k)
            {
                for (int i = n; i > k; i--)
                {
                    result *= i;
                }

                result /= Factorial(n - k);
            }
            else
            {
                for (int i = n; i > n - k; i--)
                {
                    result *= i;
                }

                result /= Factorial(k);
            }

            return result;
        }
    }
}
