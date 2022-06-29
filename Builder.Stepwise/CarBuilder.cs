using System;

namespace Creational.Builder.Stepwise
{
    public interface ISpecifyCarType
    {
        public ISpecifyWheelSize OfType(CarType type);
    }

    public interface ISpecifyWheelSize
    {
        public IBuildCar WithWheels(int size);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {
        public static ISpecifyCarType Create() => new Impl();

        private sealed class Impl :
          ISpecifyCarType,
          ISpecifyWheelSize,
          IBuildCar
        {
            private readonly Car _car = new();

            public ISpecifyWheelSize OfType(CarType type)
            {
                _car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (_car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {_car.Type}.");
                }
                _car.WheelSize = size;
                return this;
            }

            public Car Build() => _car;
        }
    }
}