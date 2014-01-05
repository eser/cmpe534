using System.Collections.Generic;
using Deduction.Abstraction;

namespace Deduction.PropositionMembers
{
    public class Or : Connective
    {
        public Or(params IPropositionMember[] parameters)
            : base(parameters)
        {
        }

        public Or(IEnumerable<IPropositionMember> parameters)
            : base(parameters)
        {
        }

        public override int ParameterCount
        {
            get
            {
                return 2;
            }
        }
        public override bool RightAssociative
        {
            get
            {
                return false;
            }
        }

        public override bool Operation(bool[] values)
        {
            return values[0] || values[1];
        }
    }
}
