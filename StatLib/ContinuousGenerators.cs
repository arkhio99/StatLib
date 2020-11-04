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
            for (int i = 0; i < n; i += 2)
            {
                double a = average;
                double s = standartDifference;
                double maxProb = 1 / (s * Math.Sqrt(2 * Math.PI));
                double u = rand.NextDouble() % maxProb;
                double log = Math.Log(u * s * Math.Sqrt(2 * Math.PI));
                double sqrt = Math.Sqrt(-2 * s * s * log);
                //Console.WriteLine($"u = {u:f4}, x = {a + sqrt:f4}, res_prob = {Norm(a + sqrt, a, s):f4}");
                //Console.WriteLine($"u = {u:f4}, x = {a - sqrt:f4}, res_prob = {Norm(a - sqrt, a, s):f4}");
                ar[i] = a + sqrt;
                ar[i + 1] = a - sqrt;
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

        private static double Generate(double average, double standartDifference, bool sign)
        {
            double a = average;
            double s = standartDifference;
            double max = 1 / (s * Math.Sqrt(2 * Math.PI));
            double u = rand.NextDouble() % max;
            double log = Math.Log(u * s * Math.Sqrt(2 * Math.PI));
            double sqrt = Math.Sqrt(-2 * s * s * log);
            double res = a + (sign ? 1 : -1) * sqrt;
            Console.WriteLine($"u = {u:f4}, x = {a + sqrt:f4}, res_prob = {Norm(res, a, s):f4}");
            return res;
        }

        private static double Norm(double x, double a, double s) => Math.Exp(-(x - a) * (x - a) / (2 * s * s)) / (s * Math.Sqrt(2 * Math.PI));
    }
}
