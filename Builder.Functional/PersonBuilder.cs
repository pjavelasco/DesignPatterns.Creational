using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creational.Builder.Functional
{
    public sealed class PersonBuilder
    {
        public List<Action<Person>> Actions = new();

        public PersonBuilder Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public Person Build()
        {
            var p = new Person();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAsA(this PersonBuilder builder, string position)
        {
            builder.Actions.Add(p =>
            {
                p.Position = position;
            });
            return builder;
        }
    }
}