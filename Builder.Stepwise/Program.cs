using System;

namespace Creational.Builder.Stepwise
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheels(18)
                .Build();
            Console.WriteLine(car);
        }
    }
}
