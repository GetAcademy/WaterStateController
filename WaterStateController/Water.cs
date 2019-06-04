namespace WaterStateController
{
    public class Water
    {
        public Water(int gram, int temperature, int proportion = 100)
        {
            
        }

        public WaterState State { get; }
        public int Temperature { get; }
    }

    public enum WaterState
    {
        Fluid, Ice, Gas, FluidAndGas, IceAndFluid
    }
}
