using System;
using System.Text;
using System.Xml.Linq;

namespace Coding.Exercise
{
    public class CodeBuilder
    {
        public string ClassName { get; set; }
        private Dictionary<string, string> FieldList = new Dictionary<string, string>();


        public CodeBuilder(string className)
        {
            ClassName = className;
        }

        public CodeBuilder AddField(string name, string type)
        {
            FieldList.Add(name, type);
            return this;
        }

        private string PrintFields()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var field in FieldList) {
                sb.AppendLine($"  public {field.Value} {field.Key};");
            }
            return sb.ToString();
        }

        public override string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"public class {ClassName}");
            stringBuilder.AppendLine("{");
            stringBuilder.Append(PrintFields());
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        //static void Main(string[] args)
        //{
        //    CodeBuilder cb = new CodeBuilder("Person");
        //    cb.AddField("Name", "string");
        //    cb.AddField("Age", "int");

        //    Console.WriteLine(cb.ToString());
        //}
    }
}
