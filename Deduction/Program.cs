using System;
using System.Collections.Generic;
using System.Text;
using Deduction.Processors;

namespace Deduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string prop = "(((Anne & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f | f | t and D";
            Dictionary<string, bool> values = new Dictionary<string, bool>()
            {
                { "A", true },
                { "B", false },
                { "C", true } // ,
                // { "D", false }
            };

            Lexer lexer = new Lexer(prop);
            var lexerOutput = lexer.Analyze();

            Parser parser = new Parser(lexerOutput);
            // parser.Grouper();

            var members = parser.Parse();
            var simplified = Simplifier.Simplify(members);
            var assigned = Evaluator.AssignValues(simplified, values);
            var evaluated = Evaluator.Evaluate(simplified, values);

            Console.WriteLine("proposition = {0}", prop);
            Console.WriteLine("simplified  = {0}", simplified.ToString());
            Console.WriteLine("assigned    = {0}", assigned.ToString());
            Console.WriteLine("evaluated   = {0}", evaluated.ToString());

            Console.Read();
        }
    }
}
