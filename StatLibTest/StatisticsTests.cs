using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using StatLib;
using System.Linq;

namespace StatLibTests
{
    [TestClass]
    public class StatisticsTests
    {
        [TestMethod]
        public void GetPearsonsNumberForBinomialDistribution_Test()
        {
            int[] xi = new int[] { 0, 1, 2, 3, 4, 5 };
            int[] frequencies = new int[] { 2, 10, 27, 32, 23, 6 };
            int N = 10;
            double probability = 0.3;
            double hiSquare = Statistics.GetPearsonsNumberForBinomialDistribution(xi, frequencies, frequencies.Sum(), N, probability);
            Assert.AreEqual(4.4398, hiSquare, 0.01);
        }

        [TestMethod]
        public void GetPearsonsNumberForNormalDistribution_Test()
        {
            double[] xi = new double[] { 5, 7, 9, 11, 13, 15, 17, 19, 21 };
            int[] ni = new int[] { 15, 26, 25, 30, 26, 21, 24, 20, 13 };
            double hiSquare = Statistics.GetPearsonsNumberForNormalDistribution(xi, ni, ni.Sum(), xi[1] - xi[0]);
            Assert.AreEqual(22.2, hiSquare, 0.3);
        }
    }
}
