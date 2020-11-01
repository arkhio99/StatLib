using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using StatLib.DiscretDistribution;
using StatLib.ContinuousDistribution;

namespace StatLib
{
    /// <summary>
    /// Класс содержит вспомогательные функции для статистики.
    /// </summary>
    public static class Statistics
    {
        /// <summary>
        /// Вычисляет среднее арифметическое.
        /// </summary>
        /// <param name="data">Массив данных.</param>
        /// <returns>Среднее арифметическое.</returns>
        public static double GetAverage(IEnumerable<double> data)
        {
            return data.Average();
        }

        /// <summary>
        /// Вычисляет среднеквадратическое отклонение.
        /// </summary>
        /// <param name="data">Массив данных.</param>
        ///  <param name="n">Массив данных.</param>
        /// <param name="average">Среднее арифметическое из этих данных.</param>
        /// <returns>Средне-квадратическое отклонение.</returns>
        public static double GetDispersion(IEnumerable<double> data, int n, double average)
        { 
            return data.Sum((x) => (x - average) * (x - average)) / (n - 1);
        }

        /// <summary>
        /// Вычисляет выборочное среднее значение.
        /// </summary>
        /// <param name="xi">Середины РАВНООТСТОЯЩИХ интервалов.</param>
        /// <param name="ni">Частоты РАВНООТСТАЯЩИХ интервалов.</param>
        /// <param name="n">Количество опытов.</param>
        /// <returns>Выборочное значение.</returns>
        public static double GetSampleAverage(IEnumerable<double> xi, IEnumerable<int> ni, int n)
        {
            return xi.Zip(ni, (x, y) => (x, y))
                .Sum((x) => (x.Item1 * x.Item2)) / n;
        }

        /// <summary>
        /// Вычисляет выборочное среднее квардратическое отклонение.
        /// </summary>
        /// <param name="xi">Середины РАВНООТСТОЯЩИХ интервалов.</param>
        /// <param name="ni">Частоты РАВНООТСТОЯЩИХ интервалов.</param>
        /// <param name="sampleAverage">Выборочное среднее значение.</param>
        /// <param name="n">Количество опытов.</param>
        /// <returns>Выборочное среднее квадратическое отклонение.</returns>
        public static double GetSampleDifference(IEnumerable<double> xi, IEnumerable<int> ni, double sampleAverage, int n)
        {
            return Math.Sqrt(xi.Zip(ni, (x, y) => (x, y)).Sum(
                (x) => x.Item2 * (x.Item1 - sampleAverage) * (x.Item1 - sampleAverage)) / n);
        }

        /// <summary>
        /// Вычисляет хи-квадрат (критерий Пирсона) для нормального распределения.
        /// </summary>
        /// <param name="averagesOfIntervals">Середина РАВНООТСТОЯЩИХ интервалов.</param>
        /// <param name="frequencies">Частоты РАВНООСТОЯЩИХ инетрвалов.</param>
        /// <param name="n">Объём выборки.</param>
        /// <param name="h">Длина интервала.</param>
        /// <returns>Критерий Пирсона.</returns>
        public static double GetPearsonsNumberForNormalDistribution(IEnumerable<double> averagesOfIntervals, IEnumerable<int> frequencies, int n, double h)
        {
            double sampleAverage = GetSampleAverage(averagesOfIntervals, frequencies, n);
            double sampleDifference = GetSampleDifference(averagesOfIntervals, frequencies, sampleAverage, n);
            IEnumerable<double> ui = from averageOfInterval in averagesOfIntervals select (averageOfInterval - sampleAverage) / sampleDifference;
            IEnumerable<double> theorFrequencies = from value in ui select n * h / sampleDifference * Normal.GetDensityOfStandartNormalDistribution(value);
            return GetPearsonNumber(frequencies, theorFrequencies);
        }

        /// <summary>
        /// Вычисляет хи-квадрат (критерий Пирсона) для биномиального распределения.
        /// </summary>
        /// <param name="frequencies">Массив частот успехов при соответствующем количестве успехов.</param>
        /// <param name="N">Количество испытаний(не путать с опытами).</param>
        /// <param name="probability">Вероятность появления события.</param>
        /// <param name="n">Количество опытов.</param>
        /// <param name="k">"Количество степеней свободы."</param>
        /// <returns>Критерий Пирсона.</returns>
        public static double GetPearsonsNumberForBinomialDistribution(int[] xi, int[] frequencies, int n, int N, double probability, out int k)
        {
            Binomial binom = new Binomial(N, probability);
            double[] probabilitiesByCountOfProvings = (from val in xi select binom.GetValue(val)).ToArray();
            double[] theorFrequencies = (from val in probabilitiesByCountOfProvings select n * val).ToArray();

            List<int> newFreqs = new List<int>();
            List<double> newTheorFreqs = new List<double>();
            int tempFreq = 0;
            double tempTheorFreq = 0;
            for(int i = 0; i < frequencies.Length; i++)
            {
                tempFreq += frequencies[i];
                tempTheorFreq += theorFrequencies[i];

                if (tempFreq > 5)
                {
                    newFreqs.Add(tempFreq);
                    newTheorFreqs.Add(tempTheorFreq);
                    tempFreq = 0;
                    tempTheorFreq = 0;
                }
            }
            
            frequencies = newFreqs.ToArray();
            theorFrequencies = newTheorFreqs.ToArray();
            k = frequencies.Length - 1;

            return GetPearsonNumber(frequencies, theorFrequencies);
        }

        public static double GetPearsonNumber(IEnumerable<int> frequencies, IEnumerable<double> theorFrequencies)
        {
            return frequencies.Zip(theorFrequencies, (x, y) => ((double)x, y))
                .Sum((x) => (x.Item1 - x.Item2) * (x.Item1 - x.Item2) / x.Item2);
        }
    }
}
