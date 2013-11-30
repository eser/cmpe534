using System;
using Deduction.Parsing;
using Deduction.Processors;

namespace Deduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string prop = "(((A & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f";

            Parser parser = new Parser(prop);
            var members = parser.Parse();
            var simplified = Simplifier.Simplify(members);
            var dumped = Dumper.GetString(simplified);

            Console.WriteLine("prop       = {0}", prop);
            Console.WriteLine("dumper     = {0}", dumped);

            Console.Read();
        }
    }
}
