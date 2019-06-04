using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaterStateController;

namespace WaterStateControllerTest
{
    [TestClass]
    public class WaterStateControllerTest
    {
        [TestMethod]
        public void Test1()
        {
            var water = new Water();
            Assert.AreEqual(WaterState.Fluid, water.State);
        }
    }
}
