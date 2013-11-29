using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Connectives;

namespace Deduction.Parsing
{
    public static class Dumper
    {
        public static string GetString(IEnumerable<IPropositionMember> members)
        {
            string final = string.Empty;

            foreach (IPropositionMember member in members)
            {
                if (member is PropositionSymbol)
                {
                    final += (member as PropositionSymbol).Letter;
                }
                else if (member is BinaryConnectiveBase)
                {
                    char? symbol = SymbolRegistry.GetSymbol(member.GetType());
                    if (symbol.HasValue)
                    {
                        final += " " + symbol.Value + " ";
                    }
                }
                else if (member is UnaryConnectiveBase)
                {
                    char? symbol = SymbolRegistry.GetSymbol(member.GetType());
                    if (symbol.HasValue)
                    {
                        final += symbol.Value;
                    }
                }
                else if (member is PropositionArray)
                {
                    final += "(" + Dumper.GetString((member as PropositionArray).Items) + ")";
                }
            }

            return final;
        }
    }
}
