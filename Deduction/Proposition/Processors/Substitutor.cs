using System;
using System.Collections.Generic;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Parsing;

namespace Deduction.Proposition.Processors
{
    public class Substitutor
    {
        protected readonly Registry registry;

        public Substitutor(Registry registry)
        {
            this.registry = registry;
        }

        public IMember Substitute(IMember node, Dictionary<string, string> table)
        {
            IMember final = node.Clone() as IMember;

            if (final is Connective)
            {
                Connective sourceConnective = final as Connective;
                RegistryMember sourceRegistryMember = this.registry.GetMemberByType(sourceConnective.GetType());

                if (table.ContainsKey(sourceRegistryMember.SymbolChar))
                {
                    RegistryMember targetRegistryMember = this.registry.GetMemberBySymbolChar(table[sourceRegistryMember.SymbolChar]);
                    sourceConnective = Activator.CreateInstance(targetRegistryMember.Type, sourceConnective.Parameters) as Connective;
                }

                for (int i = 0; i < sourceConnective.ParameterCount; i++)
                {
                    sourceConnective.Parameters[i] = this.Substitute(sourceConnective.Parameters[i], table);
                }

                return sourceConnective;
            }

            if (final is Symbol)
            {
                Symbol sourceSymbol = final as Symbol;

                if (table.ContainsKey(sourceSymbol.Letter))
                {
                    sourceSymbol = Activator.CreateInstance(sourceSymbol.GetType(), table[sourceSymbol.Letter]) as Symbol;
                }

                return sourceSymbol;
            }

            throw new Exception();
        }
    }
}
