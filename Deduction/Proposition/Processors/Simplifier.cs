using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Parsing;

namespace Deduction.Proposition.Processors
{
    public class Simplifier
    {
        protected readonly Registry registry;

        public Simplifier(Registry registry)
        {
            this.registry = registry;
        }

        public IMember Simplify(IMember node)
        {
            IMember final = node.Clone() as IMember;

            if (final is Connective)
            {
                Connective sourceConnective = final as Connective;

                for (int i = 0; i < sourceConnective.ParameterCount; i++)
                {
                    sourceConnective.Parameters[i] = this.Simplify(sourceConnective.Parameters[i]);
                }

                IMember simplified = sourceConnective.Simplify(this.registry);
                if (simplified != null)
                {
                    return simplified;
                }

                return sourceConnective;
            }

            return final;
        }
    }
}
