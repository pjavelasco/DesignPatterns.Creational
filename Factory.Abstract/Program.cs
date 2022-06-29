using System;
using System.Collections.Generic;

namespace Creational.Factory.Abstract
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 300);
            drink.Consume();

            var machine2 = new HotDrinkMachineOop();
            var drink2 = machine2.MakeDrink();
            drink2.Consume();
        }
    }

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice but I'd prefer it with milk.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is delicious!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }

    // Violates open-closed because enum
    public class HotDrinkMachine
    {
        public enum AvailableDrink 
        {
            Coffee, Tea
        }

        private readonly Dictionary<AvailableDrink, IHotDrinkFactory> _factories = new();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType($"Creational.Factory.Abstract.{drink}Factory"));
                _factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return _factories[drink].Prepare(amount);
        }
    }

    // Refactoring using reflection and OOP
    public class HotDrinkMachineOop
    {
        private List<Tuple<string, IHotDrinkFactory>> _factories = new();

        public HotDrinkMachineOop()
        {
            foreach (var t in typeof(HotDrinkMachineOop).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    _factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                    ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks");
            for (var index = 0; index < _factories.Count; index++)
            {
                var tuple = _factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i) 
                    && i >= 0
                    && i < _factories.Count)
                {
                    Console.Write("Specify amount: ");
                    s = Console.ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return _factories[i].Item2.Prepare(amount);
                    }
                }
                Console.WriteLine("Incorrect input, try again.");
            }
        }
    }    
}