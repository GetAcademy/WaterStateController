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
            Assert.AreEqual(50, water.Amount);
        }

        [TestMethod]
        public void TestWaterAtMinus20Degrees()
        {
            var water = new Water(50, -20);
            Assert.AreEqual(WaterState.Ice, water.State);
            Assert.AreEqual(-20, water.Temperature);
        }

        [TestMethod]
        public void TestWaterAt120Degrees()
        {
            var water = new Water(50, 120);
            Assert.AreEqual(WaterState.Gas, water.State);
            Assert.AreEqual(120, water.Temperature);
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
            Assert.AreEqual(100, water.Temperature);
        }

        [TestMethod]
        public void TestAddEnergy1()
        {
            var water = new Water(4, 10);
            water.AddEnergy(10);
            Assert.AreEqual(12.5, water.Temperature);
        }

        [TestMethod]
        public void TestAddEnergy2()
        {
            var water = new Water(4, -10);
            water.AddEnergy(10);
            Assert.AreEqual(-7.5, water.Temperature);
        }

        [TestMethod]
        public void TestAddEnergy3()
        {
            var water = new Water(4, -10);
            water.AddEnergy(170);
            Assert.AreEqual(0, water.Temperature);
            Assert.AreEqual(WaterState.IceAndFluid, water.State);
            Assert.AreEqual(0.5, water.ProportionFirstState);
        }

        [TestMethod]
        public void TestAddEnergy4()
        {
            var water = new Water(4, -10);
            water.AddEnergy(330);
            Assert.AreEqual(0, water.Temperature);
            Assert.AreEqual(WaterState.IceAndFluid, water.State);
            Assert.AreEqual(0, water.ProportionFirstState);
        }

        [TestMethod]
        public void TestAddEnergy5()
        {
            var water = new Water(4, -10);
            water.AddEnergy(370);
            Assert.AreEqual(10, water.Temperature);
            Assert.AreEqual(WaterState.IceAndFluid, water.State);
        }

        [TestMethod]
        public void TestAddEnergy6()
        {
            var water = new Water(4, 100, 1);
            water.AddEnergy(370);
            Assert.AreEqual(10, water.Temperature);
            Assert.AreEqual(WaterState.IceAndFluid, water.State);
        }
    }
}
