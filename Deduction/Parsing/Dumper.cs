using Deduction.Abstraction;
using Deduction.Abstraction.Connectives;
using Deduction.Abstraction.Constants;

namespace Deduction.Parsing
{
    public static class Dumper
    {
        public static string GetString(PropositionArray input)
        {
            string final = string.Empty;

            foreach (IPropositionMember member in input.Items)
            {
                if (member is BinaryConnectiveBase)
                {
                    char? symbol = SymbolRegistry.GetConnectiveSymbol(member.GetType());
                    if (symbol.HasValue)
                    {
                        final += " " + symbol.Value + " ";
                    }
                }
                else if (member is UnaryConnectiveBase)
                {
                    char? symbol = SymbolRegistry.GetConnectiveSymbol(member.GetType());
                    if (symbol.HasValue)
                    {
                        final += symbol.Value;
                    }
                }
                else if (member is PropositionSymbol)
                {
                    PropositionSymbol symbol = member as PropositionSymbol;

                    if (symbol.Negated)
                    {
                        final += "!";
                    }

                    final += symbol.Letter;
                }
                else if (member is ConstantBase)
                {
                    ConstantBase constant = member as ConstantBase;

                    if (constant.Negated)
                    {
                        final += "!";
                    }

                    char? constantChar = SymbolRegistry.GetConstantSymbol(member.GetType());
                    if (constantChar.HasValue)
                    {
                        final += constantChar.Value;
                    }
                }
                else if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    if (array.Negated)
                    {
                        final += "!";
                    }

                    final += "(" + Dumper.GetString(array) + ")";
                }
            }

            return final;
        }
    }
}
