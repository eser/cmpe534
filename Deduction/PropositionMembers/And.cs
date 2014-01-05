using System.Collections.Generic;
using Deduction.Abstraction;

namespace Deduction.PropositionMembers
{
    public class And : Connective
    {
        public And(params IPropositionMember[] parameters)
            : base(parameters)
        {
        }

        public And(IEnumerable<IPropositionMember> parameters)
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
            return values[0] && values[1];
        }
    }
}
