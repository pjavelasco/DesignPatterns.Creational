using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Creational.Singleton
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
                        
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
    }

    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private readonly Dictionary<string, int> _capitals;
        private static int _instanceCount;
        public static int Count => _instanceCount;

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing database");

            _capitals = File.ReadAllLines(Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"))
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
        }

        public int GetPopulation(string name) => _capitals[name];

        // Laziness + thread safety
        private static readonly Lazy<SingletonDatabase> _instance = new(() =>
        {
            _instanceCount++;
            return new SingletonDatabase();
        });

        public static IDatabase Instance => _instance.Value;
    }

    public class SingletonRecordFinder
    {
        public int TotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);                
            }
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private readonly IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database) => this._database = database;

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names) 
            {
                result += _database.GetPopulation(name);                
            }
            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name) => new Dictionary<string, int>
        {
            ["alpha"] = 1,
            ["beta"] = 2,
            ["gamma"] = 3
        }[name];
    }

    public class OrdinaryDatabase : IDatabase
    {
        private readonly Dictionary<string, int> _capitals;

        public OrdinaryDatabase()
        {
            Console.WriteLine("Initializing database");

            _capitals = File.ReadAllLines(Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"))
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
        }

        public int GetPopulation(string name) => _capitals[name];
    }
}
