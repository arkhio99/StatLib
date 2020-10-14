using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using StatLib.DiscretDistribution;

namespace StatLibTests
{
    [TestClass]
    public class DiscretDistributionTests
    {
        [TestMethod]
        public void Binomial_Test()
        {
            int N = 10;
            double p = 0.3;
            double[] ni = new double[] { 2, 10, 37, 32, 23, 6 };
            double[] piExpected = new double[] { 0.0282, 0.1211, 0.2335, 0.2668, 0.2001, 0.1029 };
            Binomial binomial = new Binomial(N, p);
            double[] pi = new double[ni.Length];
            for (int i = 0; i < pi.Length; i++)
            {
                pi[i] = binomial.GetValue(i);
                Console.WriteLine($"p[{i}] = {pi[i]:f4}");
                Assert.AreEqual(piExpected[i], pi[i], 0.001);
            }
        }
    }
}
