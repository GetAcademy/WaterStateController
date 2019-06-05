using System;
using System.Data.Odbc;

namespace WaterStateController
{
    public class Water
    {
        private const double CaloriesMeltIcePerGram = 80;
        private const double CaloriesEvaporateWaterPerGram = 600;

        public Water(double amount, double temperature, double? proportion = null)
        {
            Amount = amount;
            Temperature = temperature;
            State = temperature <= 0 ? WaterState.Ice
                : temperature > 100 ? WaterState.Gas
                : WaterState.Fluid;
            if (Temperature != 100 && Temperature != 0) return;

            if (proportion == null)
                throw new ArgumentException("When temperature is 0 or 100, you must provide a value for proportion");

            ProportionFirstState = proportion.Value;

            if (ProportionFirstState == 1) return;
            if (ProportionFirstState == 0) State = temperature == 0 ? WaterState.Fluid : WaterState.Gas;
            else State = temperature == 0 ? WaterState.IceAndFluid : WaterState.FluidAndGas;
        }

        public WaterState State { get; private set; }
        public double Temperature { get; private set; }
        public double Amount { get; private set; }
        public double ProportionFirstState { get; private set; }

        public void AddEnergy(double calories)
        {
            //var nextPhaseChangeTemperature = 
            //    Temperature < 0 ? 0 : 
            //    Temperature < 100 ? (int?)100 : 
            //    null;
            var caloriesPerGram = calories / Amount;
            var temperature = Temperature + caloriesPerGram;
            if (!(temperature > 0) || !(Temperature < 0))
            {
                Temperature = temperature;
                return;
            }

            State = WaterState.IceAndFluid;
            var caloriesForHeatingToZero = -Temperature * Amount;
            calories -= caloriesForHeatingToZero;
            var caloriesForMeltingEverything = CaloriesMeltIcePerGram * Amount;
            ProportionFirstState = 1 - calories / caloriesForMeltingEverything;
            Temperature = 0;
        }
    }

    public enum WaterState
    {
        Fluid, Ice, Gas, FluidAndGas, IceAndFluid
    }
}
