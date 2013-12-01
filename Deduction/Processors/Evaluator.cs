using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Constants;

namespace Deduction.Processors
{
    public static class Evaluator
    {
        public static bool Evaluate(IEnumerable<IPropositionMember> members, Dictionary<char, bool> values)
        {

            return false;
        }

        public static PropositionArray AssignValues(PropositionArray input, Dictionary<char, bool> values)
        {
            PropositionArray final = new PropositionArray();

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;
                    PropositionArray arrayMembers = Evaluator.AssignValues(array, values);

                    if (Simplifier.HasOnlyLiterals(arrayMembers))
                    {
                        final.Items.AddRange(arrayMembers.Items);
                    }
                    else
                    {
                        final.Items.Add(arrayMembers);
                    }

                    continue;
                }
                else if ((member is ConstantBase) && !(member is False || member is True))
                {
                    ConstantBase constant = member as ConstantBase;

                    if (constant.Value)
                    {
                        final.Items.Add(new True());
                    }
                    else
                    {
                        final.Items.Add(new False());
                    }
                }
                else if (member is PropositionSymbol)
                {
                    PropositionSymbol symbol = member as PropositionSymbol;

                    if (values.ContainsKey(symbol.Letter))
                    {
                        if (values[symbol.Letter])
                        {
                            final.Items.Add(new True());
                        }
                        else
                        {
                            final.Items.Add(new False());
                        }
                    }
                    else
                    {
                        final.Items.Add(member);
                    }
                }
                else
                {
                    final.Items.Add(member);
                }
            }

            return final;
        }
    }
}
