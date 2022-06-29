using System;

namespace Creational.Prototype.CopyConstructors
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));
            var jane = new Person(john);
            jane.Names[0] = "Jane";

            Console.WriteLine(john);// oops, john is called jane
            Console.WriteLine(jane);
        }
    }

    public class Address
    {
        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public class Person
    {
        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        public string[] Names { get; set; }
        public Address Address { get; set; }

        public override string ToString() => $"{nameof(Names)}: {string.Join(", ", Names)}, {nameof(Address)}: {Address}";
    }

}
