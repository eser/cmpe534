using System;
using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.DomainMembers;

namespace Deduction.Processors
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

        public PropositionArray Parse()
        {
            PropositionArray final = new PropositionArray();

            while (true) {
                char? curr = this.GetNext();
                if (!curr.HasValue)
                {
                    break;
                }

                if (curr.Value == '(')
                {
                    string insideParanthesis = this.GetInsideParanthesis();

                    Parser insideParser = new Parser(insideParanthesis);
                    PropositionArray array = new PropositionArray(insideParser.Parse());
                    final.Items.Add(array);
                }
                else if (curr.Value == ')')
                {
                    continue;
                }
                else if (char.IsWhiteSpace(curr.Value))
                {
                    continue;
                }
                else
                {
                    DomainMember domainMember = Domain.GetMemberBySymbolChar(curr.Value);
                    if (domainMember != null)
                    {
                        object[] parameters;

                        if (domainMember.Type.IsAssignableFrom(typeof(PropositionSymbol)))
                        {
                            parameters = new object[] { curr.Value, domainMember.Value, false };
                        }
                        else
                        {
                            parameters = new object[0];
                        }

                        IPropositionMember propositionMember = (IPropositionMember)Activator.CreateInstance(domainMember.Type, parameters);
                        final.Items.Add(propositionMember);
                    }
                    else
                    {
                        IPropositionMember propositionMember = (IPropositionMember)Activator.CreateInstance(typeof(PropositionSymbol), new object[] { curr.Value, null, false });
                        final.Items.Add(propositionMember);
                    }
                }
            }

            return final;
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
