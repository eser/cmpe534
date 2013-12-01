using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Constants;
using Deduction.Parsing;

namespace Deduction.Processors
{
    public static class Evaluator
    {
        public static bool Evaluate(IEnumerable<IPropositionMember> members, Dictionary<char, bool> values)
        {

            return false;
        }

        public static List<IPropositionMember> AssignValues(IEnumerable<IPropositionMember> members, Dictionary<char, bool> values)
        {
            List<IPropositionMember> final = new List<IPropositionMember>();

            foreach (IPropositionMember member in members)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    IList<IPropositionMember> arrayMembers = Evaluator.AssignValues(array.Items, values);

                    if (Simplifier.HasOnlyLiterals(arrayMembers))
                    {
                        final.AddRange(arrayMembers);
                    }
                    else
                    {
                        final.Add(new PropositionArray(arrayMembers));
                    }

                    continue;
                }
                else if ((member is ConstantBase) && !(member is False || member is True))
                {
                    ConstantBase constant = member as ConstantBase;

                    if (constant.Value)
                    {
                        final.Add(new True());
                    }
                    else
                    {
                        final.Add(new False());
                    }
                }
                else if (member is PropositionSymbol)
                {
                    PropositionSymbol symbol = member as PropositionSymbol;

                    if (values[symbol.Letter])
                    {
                        final.Add(new True());
                    }
                    else
                    {
                        final.Add(new False());
                    }
                }
                else
                {
                    final.Add(member);
                }
            }

            return final;
        }
    }
}
