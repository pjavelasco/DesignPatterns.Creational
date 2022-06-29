namespace Creational.Builder.Stepwise
{
    public enum CarType
    {
        Sedan,
        Crossover
    }

    public class Car
    {
        public CarType Type { get; set; }
        public int WheelSize { get; set; }
    }
}