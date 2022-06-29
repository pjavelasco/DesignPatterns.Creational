using System;

namespace Creational.Builder.Functional
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            var person = pb.Called("Benito").WorksAsA("Albañil").Build();
        }
    }
}
