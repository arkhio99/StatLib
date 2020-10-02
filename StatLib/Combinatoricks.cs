using System;
using System.Collections.Generic;
using System.Text;

namespace StatLib
{
    /// <summary>
    /// Библиотека, содержащая некоторые функции комбинаторики
    /// </summary>
    static class Combinatoricks
    {
        /// <summary>
        /// Вычисляет факториал числа.
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
        /// Вычисляет количество сочетаний без повторений из n элементов по k.
        /// </summary>
        /// <param name="k">Мощность сочетания.</param>
        /// <param name="n">Мощность множества.</param>
        /// <returns>Количество сочетаний без повторений.</returns>
        public static int C(int k, int n)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }
    }
}
