﻿using System;
using System.Collections;
using System.Collections.Generic;
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
        /// <param name="data">Коллекция, содержащая выборку.</param>
        /// <param name="n">Размер выборки.</param>
        public Normal(IEnumerable<double> data, int n)
        {
            average = stats.GetAverage(data);
            standDif = Math.Sqrt(stats.GetDispersion(data, n, average));
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
        public double GetProbabilityWithCondition(double x, ConditionOfProbability cond)
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
        /// Вычисляет функцию Лапласа.
        /// </summary>
        /// <param name="z">Значение.</param>
        /// <returns>Значение функции Лапласа.</returns>
        public double LaplasFunction(double z)
        {
            Func<double, double> funcBelowIntegral = (arg) => Math.Exp(-1 * arg * arg / 2);
            double result = 1 / Math.Sqrt(2 * Math.PI) *
                IntegralCalculation.CalculateIntegralByTrapeze(
                    funcBelowIntegral, z, -100, 0.1);

            return result;
        }

        /// <summary>
        /// Вычисляет значение плотности стандартного нормального закона распределения ( ф(х, 0, 1) ).
        /// </summary>
        /// <param name="x">Значение.</param>
        /// <returns>Плотность.</returns>
        public static double GetDensityOfStandartNormalDistribution(double x)
        {
            return 1 / 2.5066282746310002 * Math.Exp(-1 * x * x / 2);
        }

        public double GetDensity(double x)
        {
            return 1 / (2.5066282746310002 * standDif) * Math.Exp(-1 * (x - average) * (x - average) / (2 * standDif * standDif));
        }

        /// <summary>
        /// Вычисляет вероятность принадлежности случайной величины некоторому отрезку (minusX, plusX).
        /// </summary>
        /// <param name="plusX">Правая граница отрезка.</param>
        /// <param name="minusX">Левая граница отрезка.</param>
        /// <returns>Вероятность принадлежности слуйчаной величины некоторому отрезку.</returns>
        public double GetProbabilityBetweenValues(double minusX, double plusX)
        {
            double sqrtOfDisp = Math.Sqrt(standDif);
            return GetProbabilityWithCondition(plusX, ConditionOfProbability.RandomVariableLessThanX) - GetProbabilityWithCondition(minusX, ConditionOfProbability.RandomVariableLessThanX);
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
