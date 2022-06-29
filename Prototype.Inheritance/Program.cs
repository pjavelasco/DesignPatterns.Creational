using System;

namespace Prototype.Inheritance
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var john = new Employee
            {
                Names = new[] { "John", "Doe" },
                Address = new Address { HouseNumber = 123, StreetName = "London Road" },
                Salary = 321000
            };

            var copy = john.DeepCopy();
            copy.Names[1] = "Smith";
            copy.Address.HouseNumber++;
            copy.Salary = 123000;

            Console.WriteLine(john);
            Console.WriteLine(copy);
        }
    }

    public interface IDeepCopyable<T> where T : new()
    {
        void CopyTo(T target);

        public T DeepCopy()
        {
            T t = new();
            CopyTo(t);
            return t;
        }
    }

    public class Address : IDeepCopyable<Address>
    {
        public Address()
        {
        }
        
        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public void CopyTo(Address target)
        {
            target.StreetName = StreetName;
            target.HouseNumber = HouseNumber;
        }

        public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    public class Person : IDeepCopyable<Person>
    {
        public Person()
        {
        }
        
        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public string[] Names { get; set; }
        public Address Address { get; set; }

        public void CopyTo(Person target)
        {
            target.Names = (string[])Names.Clone();
            target.Address = Address.DeepCopy();
        }

        public override string ToString() => $"{nameof(Names)}: {string.Join(", ", Names)}, {nameof(Address)}: {Address}";
    }

    public class Employee : Person, IDeepCopyable<Employee>
    {
        public Employee()
        {
        }
        
        public Employee(string[] names, Address address, int salary) : base(names, address)
        {
            Salary = salary;
        }

        public int Salary { get; set; }

        public void CopyTo(Employee target)
        {
            base.CopyTo(target);
            target.Salary = Salary;
        }

        public override string ToString() => $"{base.ToString()}, {nameof(Salary)}: {Salary}";
    }

    public static class DeepCopyExtensions
    {
        public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new() => item.DeepCopy();

        public static T DeepCopy<T>(this T person) where T : Person, new() => ((IDeepCopyable<T>)person).DeepCopy();
    }
}
