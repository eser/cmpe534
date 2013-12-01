using System;
using System.Collections;
using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Connectives;

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

        public static bool HasOnlyLiterals(PropositionArray input)
        {
            foreach (IPropositionMember member in input.Items)
            {
                if (!(member is IPropositionValue || member is UnaryConnectiveBase))
                {
                    return false;
                }
            }

            return true;
        }

        public static Type GetCommonBinaryConnectiveType(PropositionArray input)
        {
            Type binaryConnectiveType = null;

            foreach (IPropositionMember member in input.Items)
            {
                if (!(member is BinaryConnectiveBase))
                {
                    continue;
                }

                if (binaryConnectiveType == null)
                {
                    binaryConnectiveType = member.GetType();
                    continue;
                }

                if (binaryConnectiveType != member.GetType())
                {
                    return null;
                }
            }

            return binaryConnectiveType;
        }

        public static PropositionArray RedundantParanthesis(PropositionArray input)
        {
            PropositionArray final = new PropositionArray();

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;
                    PropositionArray arrayMembers = Simplifier.RedundantParanthesis(array);

                    if (Simplifier.HasOnlyLiterals(arrayMembers))
                    {
                        final.Items.AddRange(arrayMembers.Items);
                        continue;
                    }
                }

                final.Items.Add(member);
            }

            return final;
        }

        public static PropositionArray MergeNots(PropositionArray input)
        {
            PropositionArray final = new PropositionArray();
            Stack<Not> stack = new Stack<Not>();

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    if (stack.Count % 2 > 0)
                    {
                        final.Items.Add(stack.Peek());
                    }

                    stack.Clear();

                    PropositionArray array = member as PropositionArray;
                    PropositionArray newArray = Simplifier.MergeNots(array);

                    if (Simplifier.HasOnlyLiterals(newArray))
                    {
                        final.Items.AddRange(newArray.Items);
                    }
                    else
                    {
                        final.Items.Add(newArray);
                    }
                }
                else if (member is Not)
                {
                    Not connective = member as Not;

                    if (stack.Count > 0)
                    {
                        Not lastConnective = stack.Peek();

                        if (lastConnective.GetType() == member.GetType())
                        {
                            stack.Push(connective);
                            continue;
                        }
                        else
                        {
                            if (stack.Count % 2 > 0)
                            {
                                final.Items.Add(lastConnective);
                            }

                            stack.Clear();
                        }
                    }
                    else
                    {
                        stack.Push(connective);
                    }
                }
                else
                {
                    if (stack.Count % 2 > 0)
                    {
                        final.Items.Add(stack.Peek());
                    }

                    stack.Clear();
                    final.Items.Add(member);
                }
            }

            return final;
        }

        public static PropositionArray MergeOperators(PropositionArray input)
        {
            PropositionArray final = new PropositionArray();
            Type membersCommonBinaryConnectiveType = Simplifier.GetCommonBinaryConnectiveType(input);

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    PropositionArray arrayMembers = Simplifier.MergeOperators(array);
                    Type arrayMembersCommonBinaryConnectiveType = Simplifier.GetCommonBinaryConnectiveType(arrayMembers);
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
            PropositionArray final = new PropositionArray();
            List<IPropositionMember> stack = new List<IPropositionMember>();
            Type lastConnectiveType = null;

            foreach (IPropositionMember member in input.Items)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;
                    PropositionArray arrayMembers = Simplifier.RedundantConnectives(array);

                    if (Simplifier.HasOnlyLiterals(arrayMembers))
                    {
                        stack.AddRange(arrayMembers.Items);
                    }
                    else
                    {
                        stack.Add(arrayMembers);
                    }
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
                        // continue;
                    }

                    j++;
                }
            }

            return final;
        }
    }
}
