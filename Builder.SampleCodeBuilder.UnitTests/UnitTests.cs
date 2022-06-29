using Creational.Builder.SampleCodeBuilder;
using NUnit.Framework;

namespace Builder.SampleCodeBuilder.UnitTests
{
    [TestFixture]
    public class FirstTestSuite
    {
        private static string Preprocess(string s)
        {
            return s.Trim().Replace("\r\n", "\n");
        }

        [Test]
        public void EmptyTest()
        {
            var cb = new CodeBuilder("Foo");
            Assert.That(Preprocess(cb.ToString()), Is.EqualTo("public class Foo\n{\n}"));
        }

        [Test]
        public void PersonTest()
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Assert.That(cb.ToString(),
              Is.EqualTo(
                @"public class Person
{
  public string Name;
  public int Age;
}
"
              ));
        }
    }

}