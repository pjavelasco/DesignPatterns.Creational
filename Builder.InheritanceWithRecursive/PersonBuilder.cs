using System;

namespace Creational.Builder.InheritanceWithRecursive
{
    public abstract class PersonBuilder
    {
        protected Person person = new();

        public Person Build() => person;
    }

    public class PersonInfoBuilder<TSelf> : PersonBuilder
        where TSelf : PersonInfoBuilder<TSelf>
    {
        public TSelf Called(string name)
        {
            person.Name = name;
            return (TSelf)this;
        }
    }

    public class PersonJobBuilder<TSelf> : PersonInfoBuilder<PersonJobBuilder<TSelf>>
        where TSelf : PersonJobBuilder<TSelf>
    {
        public TSelf WorksAsA(string position)
        {
            person.Position = position;
            return (TSelf)this;
        }
    }

    // Here is another inheritance level
    // Note that there is not PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<TSelf>>>
    public class PersonBirthDateBuilder<TSelf> : PersonJobBuilder<PersonBirthDateBuilder<TSelf>>
        where TSelf : PersonBirthDateBuilder<TSelf>
    {
        public TSelf Born(DateTime dateOfBirth)
        {
            person.DateOfBirth = dateOfBirth;
            return (TSelf)this;
        }
    }
}