using System.Collections.Generic;
using Deduction.Abstraction;

namespace Deduction.PropositionMembers
{
    public class Not : Connective
    {
        public Not(params IPropositionMember[] parameters)
            : base(parameters)
        {
        }

        public Not(IEnumerable<IPropositionMember> parameters)
            : base(parameters)
        {
        }

        public override int ParameterCount
        {
            get
            {
                return 1;
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
            return !values[0];
        }
    }
}
