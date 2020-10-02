using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using StatLib.DiscretDistribution;

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
            return data.Average();
        }

        /// <summary>
        /// Вычисляет среднеквадратическое отклонение.
        /// </summary>
        /// <param name="data">Массив данных.</param>
        /// <param name="average">Среднее арифметическое из этих данных.</param>
        /// <returns>Средне-квадратическое отклонение.</returns>
        public static double GetDispersion(double[] data, double average)
        { 
            return Math.Sqrt(
                data.Sum((x) => (x - average) * (x - average)) / data.Length
                );
        }

        /// <summary>
        /// Вычисляет хи-квадрат (критерий Пирсона).
        /// </summary>
        /// <param name="frequencies">Массив частот успехов при соответствующем количестве успехов.</param>
        /// <param name="N">Количество испытаний(не путать с опытами).</param>
        /// <param name="probability">Вероятность появления события.</param>
        /// <returns>Критерий Пирсона.</returns>
        public static double GetPearsonsNumberForDiscretDistribution(int[] frequencies, int N, int probability)
        {
            int[] temp = new int[frequencies.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = i;
            }

            Binomial binom = new Binomial(N, probability);
            double[] probabilitiesByCountOfProvings = (from val in temp select binom.GetValue(val)).ToArray();
            int n = frequencies.Sum();
            double[] theorFrequencies = (from val in probabilitiesByCountOfProvings select n * val).ToArray();

            return (frequencies.Zip<int, double, (double, double)>(theorFrequencies, (x, y) => ((double)x, y)))
                .Sum((x) => (x.Item1 - x.Item2) * (x.Item1 - x.Item2) / x.Item2);
        }
    }
}
