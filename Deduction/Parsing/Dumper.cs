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
                if (member is PropositionSymbol)
                {
                    final += (member as PropositionSymbol).Letter;
                }
                else if (member is BinaryConnectiveBase)
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
                else if (member is ConstantBase)
                {
                    char? symbol = SymbolRegistry.GetConstantSymbol(member.GetType());
                    if (symbol.HasValue)
                    {
                        final += symbol.Value;
                    }
                }
                else if (member is PropositionArray)
                {
                    final += "(" + Dumper.GetString(member as PropositionArray) + ")";
                }
            }

            return final;
        }
    }
}
