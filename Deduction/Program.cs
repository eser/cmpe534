using System;
using System.Collections.Generic;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Members;
using Deduction.Proposition.Parsing;
using Deduction.Proposition.Processors;

namespace Deduction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Registry registry = new Registry();
            registry.AddMembers(
                new RegistryMember("!", typeof(Not), precedence: 1, isRightAssociative: true, aliases: new string[] { "not" }),
                new RegistryMember("&", typeof(And), precedence: 4, aliases: new string[] { "and" }),
                new RegistryMember("|", typeof(Or), precedence: 5, aliases: new string[] { "or" }),
                new RegistryMember(">", typeof(Implication), precedence: 6, aliases: new string[] { "implies" }),
                new RegistryMember("=", typeof(Equivalence), precedence: 6, aliases: new string[] { "equals" }),

                new RegistryMember("[", typeof(Parenthesis), precedence: 101, closesWith: "]"),
                new RegistryMember("(", typeof(Parenthesis), precedence: 101, closesWith: ")"),

                new RegistryMember("f", typeof(Symbol), precedence: 0, value: false, aliases: new string[] { "0", "false" }),
                new RegistryMember("t", typeof(Symbol), precedence: 0, value: true, aliases: new string[] { "1", "true" })
            );

            // string prop = "(((Anne & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f | f | t and D";
            string prop = "(First | Second) & (A | B) & C";

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
            Console.WriteLine("proposition  = {0}", prop);
            Console.WriteLine("dumped root  = {0}", dumper.Dump(rootOfTree));
            Console.WriteLine("assigned     = {0}", dumper.Dump(assigned));
            Console.WriteLine("simplified   = {0}", dumper.Dump(simplified));

            Console.Read();
        }
    }
}
