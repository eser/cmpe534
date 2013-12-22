using System;
using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.DomainMembers;

namespace Deduction.Processors
{
    public static class Simplifier
    {
        public static PropositionArray Simplify(PropositionArray input)
        {
            PropositionArray simplified;

            simplified = Simplifier.RedundantParanthesis(input);
            simplified = Simplifier.MergeNots(simplified);
            simplified = Simplifier.MergeOperators(simplified);
            simplified = Simplifier.RedundantConnectives(simplified);

            return simplified;
        }

        public static PropositionArray RedundantParanthesis(PropositionArray input)
        {
            PropositionArray final = new PropositionArray()
            {
                Negated = input.Negated
            };

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;
                    PropositionArray arrayMembers = Simplifier.RedundantParanthesis(array);

                    arrayMembers.AddIntoPropositionArray(final);

                    continue;
                }

                final.Items.Add(member);
            }

            return final;
        }

        public static PropositionArray MergeNots(PropositionArray input)
        {
            PropositionArray final = new PropositionArray()
            {
                Negated = input.Negated
            };
            bool negate = false;

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    if (negate)
                    {
                        array.Negated = !array.Negated;
                        negate = false;
                    }

                    PropositionArray newArray = Simplifier.MergeNots(array);

                    newArray.AddIntoPropositionArray(final);
                }
                else if (member is Not)
                {
                    negate = !negate;
                }
                else if (member is IPropositionMemberNegable)
                {
                    IPropositionMemberNegable value = member as IPropositionMemberNegable;

                    if (negate)
                    {
                        value.Negated = !value.Negated;
                        negate = false;
                    }

                    final.Items.Add(value);
                }
                else
                {
                    final.Items.Add(member);
                    negate = false;
                }
            }

            return final;
        }

        public static PropositionArray MergeOperators(PropositionArray input)
        {
            PropositionArray final = new PropositionArray()
            {
                Negated = input.Negated
            };
            Type membersCommonBinaryConnectiveType = input.GetCommonBinaryConnectiveType();

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    PropositionArray arrayMembers = Simplifier.MergeOperators(array);
                    Type arrayMembersCommonBinaryConnectiveType = arrayMembers.GetCommonBinaryConnectiveType();
                    if (arrayMembersCommonBinaryConnectiveType != null && arrayMembersCommonBinaryConnectiveType == membersCommonBinaryConnectiveType)
                    {
                        final.Items.AddRange(arrayMembers.Items);
                    }
                    else
                    {
                        final.Items.Add(arrayMembers);
                    }

                    continue;
                }

                final.Items.Add(member);
            }

            return final;
        }

        public static PropositionArray RedundantConnectives(PropositionArray input)
        {
            PropositionArray final = new PropositionArray()
            {
                Negated = input.Negated
            };
            List<IPropositionMember> stack = new List<IPropositionMember>();
            Type lastConnectiveType = null;

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;
                    PropositionArray arrayMembers = Simplifier.RedundantConnectives(array);

                    arrayMembers.AddIntoPropositionArray(stack);
                }
                else if (member is BinaryConnectiveBase)
                {
                    BinaryConnectiveBase binaryConnective = member as BinaryConnectiveBase;
                    Type binaryConnectiveType = binaryConnective.GetType();

                    if (lastConnectiveType == null)
                    {
                        lastConnectiveType = binaryConnectiveType;
                    }
                    else if (lastConnectiveType != binaryConnectiveType)
                    {
                        if (stack.Count > 0) {
                            // remove redundants
                            stack = Simplifier.RedundantConnectivesStackSimplification(stack);

                            IPropositionMember lastItem = null;
                            foreach (IPropositionMember stackMember in stack)
                            {
                                if ((lastItem == null || !(lastItem is UnaryConnectiveBase)) && final.Items.Count > 0)
                                {
                                    final.Items.Add(Activator.CreateInstance(lastConnectiveType) as IPropositionMember);
                                }

                                final.Items.Add(stackMember);
                                lastItem = stackMember;
                            }

                            stack.Clear();
                        }

                        lastConnectiveType = binaryConnectiveType;
                    }
                }
                else
                {
                    stack.Add(member);
                }
            }

            if (stack.Count > 0)
            {
                // remove redundants
                stack = Simplifier.RedundantConnectivesStackSimplification(stack);

                IPropositionMember lastItem = null;
                foreach (IPropositionMember stackMember in stack)
                {
                    if ((lastItem == null || !(lastItem is UnaryConnectiveBase)) && final.Items.Count > 0)
                    {
                        final.Items.Add(Activator.CreateInstance(lastConnectiveType) as IPropositionMember);
                    }

                    final.Items.Add(stackMember);
                    lastItem = stackMember;
                }
            }

            return final;
        }

        private static List<IPropositionMember> RedundantConnectivesStackSimplification(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> final = new List<IPropositionMember>(members);

            for (int i = 0; i < final.Count - 1; i++)
            {
                for (int j = i + 1; j < final.Count; )
                {
                    if (final[i].Equals(final[j]))
                    {
                        final.RemoveAt(j);
                        continue;
                    }

                    j++;
                }
            }

            return final;
        }
    }
}
