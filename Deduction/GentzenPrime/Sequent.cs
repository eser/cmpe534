using System;
using System.Collections.Generic;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Parsing;

namespace Deduction.GentzenPrime
{
    public class Sequent
    {
        public const string SEQUENT_SEPERATOR = "->";
        public const string ITEM_SEPERATOR = ",";

        protected List<IMember> left;
        protected List<IMember> right;

        public Sequent()
        {
            this.left = new List<IMember>();
            this.right = new List<IMember>();
        }

        public List<IMember> Left
        {
            get
            {
                return this.left;
            }
        }

        public List<IMember> Right
        {
            get
            {
                return this.right;
            }
        }

        public static Sequent Read(Registry registry, string prop)
        {
            string[] propParts = prop.Split(new string[] { Sequent.SEQUENT_SEPERATOR }, StringSplitOptions.None);

            if (propParts.Length < 2)
            {
                return null;
            }

            Sequent sequent = new Sequent();
            Parser parser = new Parser(registry);

            for (int i = 0; i < 2; i++)
            {
                string[] sequentParts = propParts[i].Split(new string[] { Sequent.ITEM_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);

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

        public bool IsAxiom()
        {
            foreach (IMember leftMember in this.Left)
            {
                foreach (IMember rightMember in this.Right)
                {
                    if (leftMember.Equals(rightMember))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
