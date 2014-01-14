// Proposition/Members/Equivalence.cs

using System.Collections.Generic;
using Deduction.Proposition.Abstraction;

namespace Deduction.Proposition.Members
{
    public class Equivalence : Connective
    {
        public Equivalence(params IMember[] parameters)
            : base(parameters)
        {
        }

        public Equivalence(IEnumerable<IMember> parameters)
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

        public override bool Operation(bool[] values)
        {
            return !(values[0] && !values[1]) && !(!values[0] && values[1]);
        }
    }
}
