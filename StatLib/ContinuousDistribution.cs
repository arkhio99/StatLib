using System;
using stats = StatLib.Statistics;

namespace StatLib.ContinuousDistribution
{
    /// <summary>
    /// Модель нормального распределения непрерывной случайно величины.
    /// </summary>
    public class Normal
    {
        private double average;
        private double standDif;
        
        /// <summary>
        /// Конструктор, принимающий на вход выборку данных.
        /// </summary>
        /// <param name="data">Массив, содержащий выборку данных.</param>
        public Normal(double[] data)
        {
            average = stats.GetAverage(data);
            standDif = stats.GetDispersion(data, average);
        }

        /// <summary>
        /// Конструктор, содержащий среднее арифметическое и стандартное отклонение.
        /// </summary>
        /// <param name="_average">Среднее арифметическое.</param>
        /// <param name="_standDif">Стандартное отклонение.</param>
        public Normal(double _average, double _standDif)
        {
            average = _average;
            standDif = _standDif;
        }

        /// <summary>
        /// Вычисляет вероятность выполнения условия cond для значения x.
        /// </summary>
        /// <param name="x">Значение.</param>
        /// <param name="cond">Условие.</param>
        /// <returns>Вероятность.</returns>
        public double GetValue(double x, ConditionOfProbability cond)
        {
            double z = (x - average) / standDif;
            Func<double, double> funcBelowIntegral = (arg) => Math.Exp(-1 * arg * arg / 2);
            double result = 1 / Math.Sqrt(2 * Math.PI) *
                IntegralCalculation.CalculateIntegralByTrapeze(
                    funcBelowIntegral, z, -100, 0.1);

            if (cond == ConditionOfProbability.RandomVariableMoreThanX || cond == ConditionOfProbability.RandomVariableMoreOrEqualX)
            {
                result = 1 - result;
            }

            return result;
        }

        /// <summary>
        /// Вычисляет вероятность принадлежности случайной величины некоторому отрезку (minusZ, plusZ).
        /// </summary>
        /// <param name="plusZ">Правая граница отрезка.</param>
        /// <param name="minusZ">Левая граница отрезка.</param>
        /// <returns></returns>
        public double GetValueBetweenValues(double minusZ, double plusZ)
        {
            Func<double, double> funcBelowIntegral = (x) => Math.Exp(-1 * plusZ * plusZ / 2);
            double result = 1 / Math.Sqrt(2 * Math.PI) *
                IntegralCalculation.CalculateIntegralByTrapeze(
                    funcBelowIntegral, plusZ, minusZ, 0.1);

            return result;
        }
    }

    /// <summary>
    /// Условие для вероятности.
    /// </summary>
    public enum ConditionOfProbability
    { 
        RandomVariableLessThanX,
        RandomVariableLessOrEqualX,
        RandomVariableMoreThanX,
        RandomVariableMoreOrEqualX
    }
}
