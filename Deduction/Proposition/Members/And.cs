// Proposition/Members/And.cs

using System.Collections.Generic;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Parsing;

namespace Deduction.Proposition.Members
{
    public class And : Connective
    {
        public And(params IMember[] parameters)
            : base(parameters)
        {
        }

        public And(IEnumerable<IMember> parameters)
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
            return values[0] && values[1];
        }

        public override IMember Simplify(Registry registry)
        {
            Symbol firstParameter = this.Parameters[0] as Symbol;
            RegistryMember firstParameterSymbol = null;
            if (firstParameter != null)
            {
                firstParameterSymbol = registry.GetMemberBySymbolChar(firstParameter.Letter);
                if (firstParameterSymbol != null && firstParameterSymbol.Value == false)
                {
                    return new Constant("f", false);
                }
            }

            Symbol secondParameter = this.Parameters[1] as Symbol;
            RegistryMember secondParameterSymbol = null;
            if (secondParameter != null)
            {
                secondParameterSymbol = registry.GetMemberBySymbolChar(secondParameter.Letter);
                if (secondParameterSymbol != null && secondParameterSymbol.Value == false)
                {
                    return new Constant("f", false);
                }
            }

            if (firstParameter != null && secondParameter != null)
            {
                if (firstParameter.Letter == secondParameter.Letter)
                {
                    return new Symbol(firstParameter.Letter);
                }
            }

            return null;
        }
    }
}
