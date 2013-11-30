using System.Collections.Generic;

namespace Deduction.Abstraction.Connectives
{
    public abstract class UnaryConnectiveBase : IConnective
    {
        public abstract bool Operation(bool first);
        public abstract List<IPropositionMember> Simplify(params IPropositionMember[] rightItems);
    }
}
