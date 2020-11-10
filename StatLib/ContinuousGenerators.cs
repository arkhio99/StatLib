using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            for (int i = 0; i < n; i ++)
            {
                ar[i] = Generate(average, standartDifference);
            }

            IEnumerable<double> ar1 = ar.OrderBy(val => val);

            File.WriteAllLines("kek.csv",
                new string[] 
                {
                    string.Join(";", (from val in ar1 select(val.ToString())).ToArray()),
                    string.Join(";", (from val in ar1 select (Norm(val, average, standartDifference).ToString())).ToArray())
                });

            double max = ar.Max();
            double min = ar.Min();

            // Число Стерджеса
            int N = (int)(1 + 3.32218 * Math.Log10(n));
            
            averageOfIntervals = new double[N];
            frequencies = new int[N];
            h = (max + 1 - min) / N;

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
                int index = (int)Math.Floor((ar[i] - min) / h);
                frequencies[index]++;
            }
        }

        private static double Generate(double average, double standartDifference)
        {
            double a = average;
            double s = standartDifference;
            double u = rand.NextDouble();
            double res = a + s * (Math.Sqrt(-2 * Math.Log(u)) + 1);
            return res;
        }

        private static double Norm(double x, double a, double s) => Math.Exp(-(x - a) * (x - a) / (2 * s * s)) / (s * Math.Sqrt(2 * Math.PI));
    }
}
