using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatLib;

namespace StatLibTests
{
    [TestClass]
    public class ContinuousDistributionTests
    {
        [TestMethod]
        public void Normal_OneTest()
        {
            StatLib.ContinuousDistribution.Normal norm = new StatLib.ContinuousDistribution.Normal(1000, 5);
            Assert.AreEqual(norm.GetProbabilityWithCondition(1005, StatLib.ContinuousDistribution.ConditionOfProbability.RandomVariableLessThanX), 0.8413, 0.1);
        }
    }
}
