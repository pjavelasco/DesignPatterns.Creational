using System;

namespace Creational.Builder.InheritanceWithRecursive
{
    public class Person
    {
        public class Builder : PersonBirthDateBuilder<Builder>
        {
            internal Builder() { }
        }

        public static Builder New => new();

        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }

        public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}
