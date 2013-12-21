using System;
using System.Collections.Generic;
using System.Text;
using Deduction.Parsing;
using Deduction.Processors;

namespace Deduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string prop = "(((A & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f | t & D";
            Dictionary<char, bool> values = new Dictionary<char, bool>()
            {
                { 'A', true },
                { 'B', false },
                { 'C', true } // ,
                // { 'D', false }
            };

            Parser parser = new Parser(prop);
            var members = parser.Parse();
            var dumped = Dumper.GetString(members);
            var simplified = Simplifier.Simplify(members);
            // var dumped = Dumper.GetString(simplified);

            var assigned = Evaluator.AssignValues(simplified, values);
            var assignedDump = Dumper.GetString(assigned);

            var evaluated = Evaluator.Evaluate(simplified, values);
            var evaluatedDump = Dumper.GetString(evaluated);

            Console.WriteLine("proposition = {0}", prop);
            Console.WriteLine("simplified  = {0}", dumped);
            Console.WriteLine("assigned    = {0}", assignedDump);
            Console.WriteLine("evaluated   = {0}", evaluatedDump);

            Console.Read();
        }
    }
}
