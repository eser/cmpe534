using System.Collections.Generic;

namespace Deduction.Abstraction.Connectives
{
    public abstract class BinaryConnectiveBase : IConnective
    {
        public abstract bool Operation(bool first, bool second);
        public abstract List<IPropositionMember> Simplify(IPropositionMember left, params IPropositionMember[] rightItems);
    }
}
