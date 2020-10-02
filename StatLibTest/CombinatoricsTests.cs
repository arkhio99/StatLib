using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using StatLib;

namespace StatLibTests
{
    [TestClass]
    public class CombinatoricsTests
    {
        [TestMethod]
        public void C_test()
        {
            Assert.AreEqual(10, Combinatorics.C(2, 5));
        }
    }
}
