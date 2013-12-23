using System;
using System.Collections.Generic;
using Deduction.Abstraction;

namespace Deduction.Processors
{
    public class Parser
    {
        protected List<string> line;
        protected int currentPosition;

        public Parser(List<string> line)
        {
            this.line = line;
            this.currentPosition = 0;
        }

        public PropositionArray Parse()
        {
            PropositionArray final = new PropositionArray();

            this.Reset();

            while (true) {
                string curr = this.GetNext();
                if (curr == null)
                {
                    break;
                }

                if (curr == "(")
                {
                    List<string> insideParanthesis = this.GetInsideParanthesis();

                    Parser insideParser = new Parser(insideParanthesis);
                    PropositionArray array = new PropositionArray(insideParser.Parse());
                    final.Items.Add(array);
                }
                else if (curr == ")")
                {
                    continue;
                }
                else
                {
                    DomainMember domainMember = Domain.GetMemberBySymbolChar(curr);
                    if (domainMember != null)
                    {
                        object[] parameters;

                        if (domainMember.Type.IsAssignableFrom(typeof(PropositionSymbol)))
                        {
                            parameters = new object[] { curr, domainMember.Value, false };
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
                        IPropositionMember propositionMember = (IPropositionMember)Activator.CreateInstance(typeof(PropositionSymbol), new object[] { curr, null, false });
                        final.Items.Add(propositionMember);
                    }
                }
            }

            return final;
        }

        //public void Grouper()
        //{
        //    DomainMember[] domainMembers = Domain.GetMembersSorted();

        //    foreach (DomainMember domainMember in domainMembers)
        //    {
        //        string final = string.Empty;

        //        this.Reset();

        //        while (true)
        //        {
        //            string curr = this.GetNext();
        //            if (curr == null)
        //            {
        //                break;
        //            }

        //            if (curr == "(" || curr == ")")
        //            {
        //                final += curr;
        //                continue;
        //            }

        //            if (typeof(UnaryConnectiveBase).IsAssignableFrom(domainMember.Type))
        //            {
        //                if (domainMember.SymbolChar == curr)
        //                {
        //                    final += domainMember.SymbolChar + "(" + this.GetNext() + ")";
        //                }
        //                else
        //                {
        //                    final += curr;
        //                }
        //            }
        //            else
        //            {
        //                final += curr;
        //            }
        //            //else if (domainMember.Type.IsAssignableFrom(typeof(BinaryConnectiveBase)))
        //            //{
        //            //    if (domainMember.SymbolChar == curr.Value)
        //            //    {

        //            //    }
        //            //}
        //        }

        //        this.line = final;
        //    }

        //    Console.WriteLine(this.line);
        //}

        protected string GetNext()
        {
            if (this.currentPosition >= this.line.Count)
            {
                return null;
            }

            return this.line[this.currentPosition++];
        }

        protected void Reset()
        {
            this.currentPosition = 0;
        }

        protected List<string> GetInsideParanthesis()
        {
            int paranthesisCount = 1;
            int i = this.currentPosition;

            for (; i < this.line.Count; i++)
            {
                if (this.line[i] == "(")
                {
                    paranthesisCount++;
                    continue;
                }
                else if (this.line[i] == ")")
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
                List<string> result = this.line.GetRange(this.currentPosition, i - this.currentPosition);
                this.currentPosition = i;

                return result;
            }

            throw new ArgumentException();
        }
    }
}
