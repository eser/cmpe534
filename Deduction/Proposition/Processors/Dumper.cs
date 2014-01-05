using System.Text;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Parsing;

namespace Deduction.Proposition.Processors
{
    public class Dumper
    {
        protected readonly Registry registry;

        public Dumper(Registry registry)
        {
            this.registry = registry;
        }

        public string Dump(IMember input)
        {
            StringBuilder output = new StringBuilder();

            if (input is Symbol)
            {
                output.Append((input as Symbol).Letter);
            }
            else if (input is Connective)
            {
                Connective connective = (input as Connective);
                RegistryMember registryMember = registry.GetMemberByType(connective.GetType());

                if (connective.ParameterCount == 1)
                {
                    output.Append(registryMember.SymbolChar);
                    output.Append("(");
                    output.Append(this.Dump(connective.Parameters[0]));
                    output.Append(")");
                }
                else
                {
                    output.Append("(");
                    for (int i = 0; i < connective.ParameterCount; i++)
                    {
                        if (i != 0)
                        {
                            output.Append(" ");
                            output.Append(registryMember.SymbolChar);
                            output.Append(" ");
                        }

                        output.Append(this.Dump(connective.Parameters[i]));
                    }
                    output.Append(")");
                }
            }

            return output.ToString();
        }
    }
}
