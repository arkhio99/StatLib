using System;
using StatLib.Generators;
using StatLib;

namespace KM1
{
    class Program
    {
        static void Main(string[] args)
        {
            double p = 0.6;
            int N = 10;
            int n = 100;
            int[] frequencies = Discret.Binomial(p, N, n);
            int[] xi = new int[11];
            Console.WriteLine($"Вероятность успеха - {p}");
            Console.WriteLine($"Количество испытаний - {N}");
            Console.WriteLine($"Количество опытов - {n}");
            Console.Write("Распределение - {");
            for (int i = 0; i < frequencies.Length; i ++)
            {
                xi[i] = i;
                Console.Write($"{frequencies[i]} ");
            }

            Console.Write("}\n");

            double hiSquare = Statistics.GetPearsonsNumberForBinomialDistribution(xi, frequencies, 100, 10, p, out int k);
            Console.WriteLine($"Критерий Пирсона - {hiSquare}");
            Console.WriteLine($"Количество степеней свободы - {k}");
            Console.WriteLine("\n\n\n\n");

            double average = 4;
            double standDif = 10;
            int n1 = 1000;
            Console.WriteLine($"Среднее - {average:f3}");
            Console.WriteLine($"Стандартное отклонение - {standDif:f3}");
            Console.WriteLine($"Количество опытов - {n1}");
            Continuous.Normal(average, standDif, n1, out double[] averagesOfIntervals, out int[] freqs, out double h);
            Console.WriteLine("Середина\tЧастота");
            for (int i = 0; i < averagesOfIntervals.Length; i++)
            {
                Console.WriteLine($"{averagesOfIntervals[i]:f3}\t\t{freqs[i]}");
            }

            Console.WriteLine($"Критерий Пирсона - {Statistics.GetPearsonsNumberForNormalDistribution(averagesOfIntervals, freqs, n, h)}");
        }
    }
}
