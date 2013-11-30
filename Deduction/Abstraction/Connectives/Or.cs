using System.Collections.Generic;

namespace Deduction.Abstraction.Connectives
{
    public class Or : BinaryConnectiveBase
    {
        public override bool Operation(bool first, bool second)
        {
            return first || second;
        }

        public override List<IPropositionMember> Simplify(IPropositionMember left, params IPropositionMember[] rightItems)
        {
            return null;
        }
    }
}
