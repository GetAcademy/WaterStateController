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
            if (Temperature < 0) calories = HeatTo(calories, 0);
            if (Temperature == 0 && State != WaterState.Fluid) calories = DoStateChangeAsMuchAsPossible(calories);
            if (Temperature < 100) calories = HeatTo(calories, 100);
            if (Temperature == 100 && State != WaterState.Gas) calories = DoStateChangeAsMuchAsPossible(calories);
            HeatMax(calories);
        }

        private double DoStateChangeAsMuchAsPossible(double calories)
        {
            if (calories <= 0) return 0;
            if (Temperature != 0 && Temperature != 100) throw new ApplicationException("Cannot do state change when temperature is not 0 or 100.");

            var isMeltingIce = Temperature == 0;
            var stateChangeEnergyPerGram = isMeltingIce ? CaloriesMeltIcePerGram : CaloriesEvaporateWaterPerGram;
            var caloriesNeeded = stateChangeEnergyPerGram * Amount;

            if (calories >= caloriesNeeded)
            {
                State = isMeltingIce ? WaterState.Fluid : WaterState.Gas;
                return calories - caloriesNeeded;
            }

            ProportionFirstState = 1 - calories / caloriesNeeded;
            State = isMeltingIce ? WaterState.IceAndFluid : WaterState.FluidAndGas;
            return 0;
        }

        private double HeatTo(double calories, int temperature)
        {
            if (calories <= 0) return 0;
            var caloriesForHeating = (temperature - Temperature) * Amount;
            if (!(calories >= caloriesForHeating)) return HeatMax(calories);
            Temperature = temperature;
            return calories - caloriesForHeating;
        }

        private double HeatMax(double calories)
        {
            if (calories <= 0) return 0;
            var temperatureChange = calories / Amount;
            Temperature += temperatureChange;
            return 0;
        }
    }

    public enum WaterState
    {
        Fluid, Ice, Gas, FluidAndGas, IceAndFluid
    }
}
