using System;

namespace StatLib.Generators
{
    public static class Discret
    {
        /// <summary>
        /// Генерирует биномиально распределённую случайную переменную.
        /// </summary>
        /// <param name="p">Вероятность успеха.</param>
        /// <param name="N">Количество испытаний.</param>
        /// <param name="n">Количество опытов(1 опыт - N испытаний).</param>
        /// <returns>Массив частот.</returns>
        public static int[] Binomial(double p, int N, int n)
        {
            Random random = new Random();
            int[] frequencies = new int[N + 1];
            for (int i = 0; i < N + 1; i++)
            {
                frequencies[i] = 0;
            }

            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < N; j++)
                {
                    count += random.NextDouble() < p ? 1 : 0;
                }

                frequencies[count]++;
            }

            return frequencies;
        }
    }
}
