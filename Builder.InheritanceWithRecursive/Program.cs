using System;

namespace Creational.Builder.InheritanceWithRecursive
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var me = Person.New
                .Called("Benito")
                .WorksAsA("Albañil")
                .Born(DateTime.UtcNow)
                .Build();
            Console.WriteLine(me);
        }
    }
}
