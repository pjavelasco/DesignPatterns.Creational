using System.Collections.Generic;
using System.Text;

namespace Creational.Builder.SampleCodeBuilder
{
    public class CodeBuilder
    {
        private readonly Class _class = new();

        public CodeBuilder(string className)
        {
            _class.Name = className;
        }

        public CodeBuilder AddField(string fieldName, string fieldType)
        {
            _class.Fields.Add(new Field
            {
                Name = fieldName,
                Type = fieldType
            });
            return this;
        }

        public override string ToString() => _class.ToString();
    }
    
    class Field
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"public {Type} {Name};";
    }

    class Class
    {
        public string Name { get; set; }
        public List<Field> Fields { get; set; } = new();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {Name}");
            sb.AppendLine("{");
            foreach (var field in Fields)
            {
                sb.AppendLine($"  {field}");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
