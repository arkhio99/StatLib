using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using StatLib;

namespace StatLibTests
{
    [TestClass]
    public class StatisticsTests
    {
        [TestMethod]
        public void GetPearsonsNumberForDiscretDistribution_Test()
        {
            int[] frequencies = new int[] { 2, 10, 27, 32, 23, 6 };
            int N = 10;
            double probability = 0.3;
            double hiSquare = Statistics.GetPearsonsNumberForDiscretDistribution(frequencies, N, probability);
            Assert.AreEqual(4.4398, hiSquare, 0.01);
        }
    }
}
