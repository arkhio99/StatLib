using System;
using System.Collections.Generic;
using System.Text;

namespace StatLib
{
    /// <summary>
    /// Класс содержит вспомогательные функции для статистики.
    /// </summary>
    static class Statistics
    {
        /// <summary>
        /// Вычисляет среднее арифметическое(мат.ожидание).
        /// </summary>
        /// <param name="data">Массив данных.</param>
        /// <returns>Среднее арифметическое.</returns>
        public static double GetAverage(double[] data)
        {
            double result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                result += data[i];
            }

            return result / data.Length;
        }

        /// <summary>
        /// Вычисляет среднеквадратическое отклонение.
        /// </summary>
        /// <param name="data">Массив данных.</param>
        /// <param name="average">Среднее арифметическое из этих данных.</param>
        /// <returns>Средне-квадратическое отклонение.</returns>
        public static double GetDispersion(double[] data, double average)
        {
            double result = 0;
            for (int i = 0; i < data.Length; i++)
            {
                result += (data[i] - average) * (data[i] - average);
            }

            result /= data.Length;

            return Math.Sqrt(result);
        }
    }
}
