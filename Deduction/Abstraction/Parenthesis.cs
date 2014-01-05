using System.Collections.Generic;

namespace Deduction.Abstraction
{
    public class Parenthesis : Connective
    {
        public Parenthesis(params IPropositionMember[] parameters)
            : base(parameters)
        {
        }

        public Parenthesis(IEnumerable<IPropositionMember> parameters)
            : base(parameters)
        {
        }

        public override int ParameterCount
        {
            get
            {
                return 0;
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
            return false;
        }
    }
}
