using System;

namespace Creational.Prototype.ICloneableIsBad
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));
            
            var jane = (Person)john.Clone();
            jane.Address.HouseNumber = 321; // oops, John is now at 321

            // but clone is typically shallow copy
            jane.Names[0] = "Jane";

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }

    public class Address : ICloneable
    {
        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";

        public object Clone() => new Address(StreetName, HouseNumber);
    }

    public class Person : ICloneable
    {
        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public string[] Names { get; set; }
        public Address Address { get; set; }

        public override string ToString() => $"{nameof(Names)}: {string.Join(", ", Names)}, {nameof(Address)}: {Address}";

        public object Clone() => new Person(Names, Address);
    }
}
