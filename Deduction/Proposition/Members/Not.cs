// Proposition/Members/Not.cs

using System.Collections.Generic;
using Deduction.Proposition.Abstraction;

namespace Deduction.Proposition.Members
{
    public class Not : Connective
    {
        public Not(params IMember[] parameters)
            : base(parameters)
        {
        }

        public Not(IEnumerable<IMember> parameters)
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

        public override bool Operation(bool[] values)
        {
            return !values[0];
        }
    }
}
