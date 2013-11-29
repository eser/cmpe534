using System;
using Deduction.Parsing;

namespace Deduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string prop = "(A & B) | (B & C) | (!C & D)";
            Parser parser = new Parser(prop);
            var members = parser.Parse();

            Console.WriteLine("prop   = {0}", prop);
            Console.WriteLine("dumper = {0}", Dumper.GetString(members));

            Console.Read();
        }
    }
}
