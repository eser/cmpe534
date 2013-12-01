using System;
using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Connectives;
using Deduction.Abstraction.Constants;

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

        public PropositionArray Parse()
        {
            PropositionArray final = new PropositionArray();

            while (true) {
                char? curr = this.GetNext();
                if (!curr.HasValue)
                {
                    break;
                }

                if (char.IsUpper(curr.Value))
                {
                    PropositionSymbol symbol = new PropositionSymbol(curr.Value);
                    final.Items.Add(symbol);
                }
                else if (curr.Value == '(')
                {
                    string insideParanthesis = this.GetInsideParanthesis();

                    Parser insideParser = new Parser(insideParanthesis);
                    PropositionArray array = new PropositionArray(insideParser.Parse());
                    final.Items.Add(array);
                }
                else
                {
                    bool found = false;

                    foreach (KeyValuePair<char, Type> pair in SymbolRegistry.Instance.Connectives)
                    {
                        if (curr.Value == pair.Key)
                        {
                            IConnective connectiveInstance = (IConnective)Activator.CreateInstance(pair.Value);
                            final.Items.Add(connectiveInstance);

                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        foreach (KeyValuePair<char, Type> pair in SymbolRegistry.Instance.Constants)
                        {
                            if (curr.Value == pair.Key)
                            {
                                IConstant constantInstance = (IConstant)Activator.CreateInstance(pair.Value);
                                final.Items.Add(constantInstance);

                                found = true;
                                break;
                            }
                        }
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
