﻿using System;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Members;
using Deduction.Proposition.Parsing;

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
            string prop = "(A | B) & C";

            Lexer lexer = new Lexer(registry, prop);
            var tokens = lexer.Analyze();

            Parser parser = new Parser(registry);
            var rootOfTree = parser.Parse(tokens);

            Dumper dumper = new Dumper(registry);
            var dumped = dumper.Dump(rootOfTree);

            Console.WriteLine("proposition = {0}", prop);
            Console.WriteLine("dumped      = {0}", dumped);

            Console.Read();
        }
    }
}
