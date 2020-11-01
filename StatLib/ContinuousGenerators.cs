using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace StatLib.Generators
{
    public static class Continuous
    {
        private static Random rand = new Random();
        public static void Normal(double average, double standartDifference, int n, out double[] averageOfIntervals, out int[] frequencies, out double h)
        {
            double[] ar = new double[n];
            for (int i = 0; i < n; i++)
            {
                ar[i] = Generate(average, standartDifference);
            }

            double max = ar.Max();
            double min = ar.Min();

            // Число Стерджеса
            int N = (int)(1 + 3.32218 * Math.Log10(n));
            
            averageOfIntervals = new double[N];
            frequencies = new int[N];
            h = (max - min) / N;

            averageOfIntervals[0] = min + h / 2;
            for (int i = 1; i < N; i++)
            {
                averageOfIntervals[i] = averageOfIntervals[i - 1] + h;
            }

            for (int i = 0; i < N; i++)
            {
                frequencies[i] = 0;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (ar[i] < averageOfIntervals[j] + h / 2)
                    {
                        frequencies[j]++;
                        break;
                    }
                }
            }
        }

        private static double Generate(double average, double standartDifference)
        {
            double a = average;
            double s = standartDifference;
            double max = 1 / (s * Math.Sqrt(2 * Math.PI));
            double u = rand.NextDouble() % max;
            double sqrt = Math.Sqrt(-2 * s * s * Math.Log(u * s * Math.Sqrt(2 * Math.PI)));
            return a + sqrt;
        }
    }
}
