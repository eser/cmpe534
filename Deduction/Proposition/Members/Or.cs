using System.Collections.Generic;
using Deduction.Proposition.Abstraction;

namespace Deduction.Proposition.Members
{
    public class Or : Connective
    {
        public Or(params IMember[] parameters)
            : base(parameters)
        {
        }

        public Or(IEnumerable<IMember> parameters)
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
