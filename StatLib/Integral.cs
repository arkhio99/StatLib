using System;
using System.Collections.Generic;
using System.Text;

namespace StatLib
{
    /// <summary>
    /// Содержит в себе функции для вычисления определённых интегралов.
    /// </summary>
    public static class IntegralCalculation
    {
        /// <summary>
        /// Приблизительно вычисляет определённый интеграл методом трапеций.
        /// </summary>
        /// <param name="function">Функция, которую надо проинтегрировать.</param>
        /// <param name="b">Верхний предел определённого интеграла.</param>
        /// <param name="a">Нижний предел определённого интеграла.</param>
        /// <param name="width">Ширина трапеции.</param>
        /// <returns>Приблизительное значение определённого интеграла.</returns>
        public static double CalculateIntegralByTrapeze(Func<double, double> function, double b, double a, double width)
        {
            double widthOfLastTrapeze = (b - a) % width;
            int its = (int)((b - a - widthOfLastTrapeze) / width);
            double leftEdge = a;
            double lastResult = function(leftEdge);
            double result = 0;
            for (int i = 0; i < its; i++)
            {
                double leftResult = lastResult;
                double rightResult = function(leftEdge + width);

                if (leftResult >= 0 && rightResult >= 0)
                {
                    result += (leftResult + rightResult) / 2 * width;
                }
                leftEdge += width;
                lastResult = rightResult;
            }

            result += (function(leftEdge) + function(leftEdge + widthOfLastTrapeze)) / 2 * widthOfLastTrapeze;

            return result;
        }
    }
}
