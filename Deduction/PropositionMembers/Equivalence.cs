using System.Collections.Generic;
using Deduction.Abstraction;

namespace Deduction.PropositionMembers
{
    public class Equivalence : Connective
    {
        public Equivalence(params IPropositionMember[] parameters)
            : base(parameters)
        {
        }

        public Equivalence(IEnumerable<IPropositionMember> parameters)
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
            return !(values[0] && !values[1]) && !(!values[0] && values[1]);
        }
    }
}
