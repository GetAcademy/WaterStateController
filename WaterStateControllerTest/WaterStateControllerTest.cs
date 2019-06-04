using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaterStateController;

namespace WaterStateControllerTest
{
    [TestClass]
    public class WaterStateControllerTest
    {
        [TestMethod]
        public void TestWaterAt20Degrees()
        {
            var water = new Water(50, 20);
            Assert.AreEqual(WaterState.Fluid, water.State);
            Assert.AreEqual(20, water.Temperature);
        }

        [TestMethod]
        public void TestIceAtMinus20Degrees()
        {
            var water = new Water(50, -20);
            Assert.AreEqual(WaterState.Ice, water.State);
            Assert.AreEqual(-20, water.Temperature);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "When temperature is 0 or 100, you must provide a value for proportion")]
        public void TestWaterAt100DegreesWithoutProportion()
        {
            var water = new Water(50, 100);
        }

        [TestMethod]
        public void TestWaterAt100Degrees()
        {
            var water = new Water(50, 100, 30);
            Assert.AreEqual(WaterState.FluidAndGas, water.State);
            Assert.AreEqual(20, water.Temperature);
        }

        
    }
}
