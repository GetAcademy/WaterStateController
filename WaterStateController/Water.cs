namespace WaterStateController
{
    public class Water
    {
        public Water(int gram, int temperature, int proportion = 100)
        {
            
        }

        public WaterState State { get; }
        public int Temperature { get; }
        public int Amount { get; }
        public double ProportionFirstState { get; }

        public void AddEnergy(int i)
        {
            
        }
    }

    public enum WaterState
    {
        Fluid, Ice, Gas, FluidAndGas, IceAndFluid
    }
}
