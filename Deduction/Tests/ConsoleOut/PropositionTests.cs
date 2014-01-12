using System.Collections.Generic;
using System.IO;
using Deduction.Proposition.Parsing;
using Deduction.Proposition.Processors;

namespace Deduction.Tests.ConsoleOut
{
    public class PropositionTests
    {
        public static void Test(TextWriter output, Registry registry, string prop)
        {
            Lexer lexer = new Lexer(registry, prop);
            var tokens = lexer.Analyze();

            Parser parser = new Parser(registry);
            var rootOfTree = parser.Parse(tokens);

            Substitutor substitutor = new Substitutor(registry);
            var table = new Dictionary<string, string>()
            {
                // { "&", "=" },
                { "Second", "First" },
                { "B", "t" },
            };
            var assigned = substitutor.Substitute(rootOfTree, table);

            Simplifier simplifier = new Simplifier(registry);
            var simplified = simplifier.Simplify(assigned);

            Dumper dumper = new Dumper(registry);
            output.WriteLine("proposition               = {0}", prop);
            output.WriteLine("Dumper.Dump()             = {0}", dumper.Dump(rootOfTree));
            output.WriteLine("Substitutor.Substitute()  = {0}", dumper.Dump(assigned));
            output.WriteLine("Simplifier.Simplify()     = {0}", dumper.Dump(simplified));
        }
    }
}
