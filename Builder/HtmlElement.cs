using System.Collections.Generic;
using System.Text;

namespace Creational.Builder
{
    internal class HtmlElement
    {
        private const int _indentSize = 2;
        public List<HtmlElement> Elements = new();

        public string Name { get; set; }
        public string Text { get; set; }

        public HtmlElement()
        {
        }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', _indentSize * indent);
            sb.Append($"{i}<{Name}>\n");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', _indentSize * (indent + 1)));
                sb.Append(Text);
                sb.Append('\n');
            }

            foreach (var e in Elements)
            {
                sb.Append(e.ToStringImpl(indent + 1));
            }

            sb.Append($"{i}</{Name}>\n");
            return sb.ToString();
        }

        public override string ToString() => ToStringImpl(0);
    }
}