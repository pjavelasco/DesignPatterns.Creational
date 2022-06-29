using System.Text;
using static System.Console;

namespace Creational.Builder
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // If you want to build a simple HTML paragraph using StringBuilder
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            WriteLine(sb);

            // Now I want an HTML list with 2 words in it
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            WriteLine(sb);
            sb.Clear();

            // Non-fluent builder
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            WriteLine(builder.ToString());

            // Fluent builder
            builder.Clear();
            builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
            WriteLine(builder);
        }
    }
}
