using System;
using Deduction.GentzenPrime.Abstraction;
using Deduction.Proposition.Parsing;

namespace Deduction.GentzenPrime.Processors
{
    public class SequentReader
    {
        public static Sequent Read(Registry registry, string prop)
        {
            string[] propParts = prop.Split(new string[] { Sequent.SEQUENT_SEPARATOR }, 2, StringSplitOptions.None);

            if (propParts.Length < 2)
            {
                propParts = new string[] { "", propParts[0] };
            }

            Sequent sequent = new Sequent();
            Parser parser = new Parser(registry);

            for (int i = 0; i < 2; i++)
            {
                string[] sequentParts = propParts[i].Split(new string[] { Sequent.ITEM_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string sequentPart in sequentParts)
                {
                    Lexer lexer = new Lexer(registry, sequentPart);
                    var tokens = lexer.Analyze();
                    var root = parser.Parse(tokens);

                    if (i == 0)
                    {
                        sequent.Left.Add(root);
                    }
                    else
                    {
                        sequent.Right.Add(root);
                    }
                }
            }

            return sequent;
        }
    }
}
