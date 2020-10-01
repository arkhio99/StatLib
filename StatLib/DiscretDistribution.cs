using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using comb = StatLib.Combinatoricks;

namespace StatLib.DiscretDistribution
{
    /// <summary>
    /// Модель биномиального распределения.
    /// </summary>
    public class Binomial
    {
        private int n;
        private double p;

        /// <summary>
        /// Конструктор класса модели биномиального распределения.
        /// </summary>
        /// <param name="how">Количество испытаний.</param>
        /// <param name="probability">Вероятность исхода.</param>
        public Binomial(int how, double probability)
        {
            n = how;
            p = probability;
        }

        /// <summary>
        /// Вычисляет вероятность успеха по количеству испытаний.
        /// </summary>
        /// <param name="i">Количество испытаний.</param>
        /// <returns>Вероятность.</returns>
        public double GetValue(int i)
        {
            return comb.C(i, n) * Math.Pow(p, i) * Math.Pow(1 - p, n - i);
        }
    }
}
