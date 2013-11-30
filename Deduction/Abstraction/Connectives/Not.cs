using System.Collections.Generic;

namespace Deduction.Abstraction.Connectives
{
    public class Not : UnaryConnectiveBase
    {
        public override bool Operation(bool first)
        {
            return !first;
        }

        public override List<IPropositionMember> Simplify(params IPropositionMember[] rightItems)
        {
            return null;
        }
    }
}
