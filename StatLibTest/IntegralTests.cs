using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatLib;

namespace StatLibTests
{
    [TestClass]
    public class IntegralTests
    {
        [TestMethod]
        public void CalculateIntegralByDifferentTrapeze_Test()
        {
            double result = IntegralCalculation.CalculateIntegralByTrapeze(
                (x) => x * x, 2, -2, 0.01);

            Assert.AreEqual(16.0 / 3, result, 0.01);
        }
    }
}
