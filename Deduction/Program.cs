using System;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Members;
using Deduction.Proposition.Parsing;
using Deduction.Tests.ConsoleOut;

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

            // proposition tests including parsing, valuation, simplification.
            // string prop = "(((Anne & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f | f | t and D";
            string prop = "(First | Second) & (A | B) & C";
            PropositionTests.Test(Console.Out, registry, prop);

            // sequent tests including parsing
            // string sequent = "A, (First | Second) & (A | B) & C -> A, B, C";
            string sequent = "B | C, D | E -> A & B, C | D";
            SequentTests.Test(Console.Out, registry, sequent);

            Console.Read();
        }
    }
}
