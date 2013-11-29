using System;
using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Connectives;

namespace Deduction.Parsing
{
    public class Parser
    {
        protected readonly string line;
        protected int currentPosition;

        public Parser(string line)
        {
            this.line = line;
            this.currentPosition = 0;
        }

        public IEnumerable<IPropositionMember> Parse()
        {
            List<IPropositionMember> members = new List<IPropositionMember>();

            while (true) {
                char? curr = this.GetNext();
                if (!curr.HasValue)
                {
                    break;
                }

                if (char.IsLetter(curr.Value))
                {
                    PropositionSymbol symbol = new PropositionSymbol(curr.Value);
                    members.Add(symbol);
                }
                else if (curr.Value == '(')
                {
                    string insideParanthesis = this.GetInsideParanthesis();

                    Parser insideParser = new Parser(insideParanthesis);
                    PropositionArray array = new PropositionArray(insideParser.Parse());
                    members.Add(array);
                }
                else
                {
                    foreach (KeyValuePair<char, Type> pair in SymbolRegistry.Instance.Connectives)
                    {
                        if (curr.Value == pair.Key)
                        {
                            IConnective connectiveInstance = (IConnective)Activator.CreateInstance(pair.Value);
                            members.Add(connectiveInstance);
                            break;
                        }
                    }
                }
            }

            return members;
        }

        protected char? GetNext()
        {
            if (this.currentPosition >= this.line.Length)
            {
                return null;
            }

            return this.line[this.currentPosition++];
        }

        protected string GetInsideParanthesis()
        {
            int paranthesisCount = 1;
            int i = this.currentPosition;

            for (; i < this.line.Length; i++)
            {
                if (this.line[i] == '(')
                {
                    paranthesisCount++;
                    continue;
                }
                else if (this.line[i] == ')')
                {
                    paranthesisCount--;
                }

                if (paranthesisCount == 0)
                {
                    break;
                }
            }

            if (paranthesisCount == 0)
            {
                string result = this.line.Substring(this.currentPosition, i - this.currentPosition);
                this.currentPosition = i;

                return result;
            }

            throw new ArgumentException();
        }
    }
}
