using System.Collections.Generic;

namespace Deduction.Proposition.Abstraction
{
    public class Parenthesis : Connective
    {
        public Parenthesis(params IMember[] parameters)
            : base(parameters)
        {
        }

        public Parenthesis(IEnumerable<IMember> parameters)
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
