using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Creational.Prototype.ThroughSerialization
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var john = new Person
            {
                Names = new[] { "John", "Doe" },
                Address = new Address { HouseNumber = 123, StreetName = "London Road" }                
            };

            //var copy = john.DeepCopy();
            var copy = john.DeepCopyXml();
            copy.Names[1] = "Smith";
            copy.Address.HouseNumber++;

            Console.WriteLine(john);
            Console.WriteLine(copy);
        }
    }

    [Serializable]
    public class Address
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

        public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }

    [Serializable]
    public class Person
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

        public override string ToString() => $"{nameof(Names)}: {string.Join(", ", Names)}, {nameof(Address)}: {Address}";
    }

    public static class ExtensionMethods
    {
        // Obsolete and crashes without [Serializable]
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            var copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }
        
        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }
}
