using System;
using System.Collections.Generic;
using Deduction.Abstraction.Constants;

namespace Deduction.Abstraction.Connectives
{
    public class And : BinaryConnectiveBase
    {
        public override bool Operation(bool first, bool second)
        {
            return first && second;
        }

        public override List<IPropositionMember> Simplify(IPropositionMember left, params IPropositionMember[] rightItems)
        {
            if (left is PropositionSymbol) {
                PropositionSymbol leftSymbol = left as PropositionSymbol;

                if (rightItems.Length > 0 && rightItems[0] is PropositionSymbol) {
                    PropositionSymbol rightSymbol = rightItems[0] as PropositionSymbol;

                    if (leftSymbol.Letter == rightSymbol.Letter)
                    {
                        List<IPropositionMember> members = new List<IPropositionMember>(rightItems);
                        members.RemoveAt(0);

                        return members;
                    }
                }
            }

            return null;
        }
    }
}
