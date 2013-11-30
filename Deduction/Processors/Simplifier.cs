using System;
using System.Collections;
using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Connectives;
using Deduction.Abstraction.Constants;

namespace Deduction.Processors
{
    public class Simplifier
    {
        public static List<IPropositionMember> Simplify(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> simplified;

            simplified = Simplifier.RedundantParanthesis(members);
            simplified = Simplifier.RedundantNots(simplified);
            simplified = Simplifier.MergeOperators(simplified);
            simplified = Simplifier.RedundantConnectives(simplified);

            return simplified;
        }

        public static bool HasOnlyLiterals(IEnumerable<IPropositionMember> members)
        {
            foreach (IPropositionMember arrayMember in members)
            {
                if (!(arrayMember is PropositionSymbol || arrayMember is UnaryConnectiveBase || arrayMember is PropositionArray || arrayMember is ConstantBase))
                {
                    return false;
                }
            }

            return true;
        }

        public static Type GetCommonBinaryConnectiveType(IEnumerable<IPropositionMember> members)
        {
            Type binaryConnectiveType = null;

            foreach (IPropositionMember arrayMember in members)
            {
                if (!(arrayMember is BinaryConnectiveBase))
                {
                    continue;
                }

                if (binaryConnectiveType == null)
                {
                    binaryConnectiveType = arrayMember.GetType();
                    continue;
                }

                if (binaryConnectiveType != arrayMember.GetType())
                {
                    return null;
                }
            }

            return binaryConnectiveType;
        }

        public static List<IPropositionMember> RedundantParanthesis(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> final = new List<IPropositionMember>();

            foreach (IPropositionMember member in members)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    IList<IPropositionMember> arrayMembers = Simplifier.RedundantParanthesis(array.Items);

                    if (Simplifier.HasOnlyLiterals(arrayMembers))
                    {
                        final.AddRange(arrayMembers);
                        continue;
                    }
                }

                final.Add(member);
            }

            return final;
        }

        public static List<IPropositionMember> RedundantNots(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> final = new List<IPropositionMember>();
            Stack<Not> stack = new Stack<Not>();

            foreach (IPropositionMember member in members)
            {
                if (member is PropositionArray)
                {
                    if (stack.Count % 2 > 0)
                    {
                        final.Add(stack.Peek());
                    }

                    stack.Clear();

                    PropositionArray array = member as PropositionArray;
                    List<IPropositionMember> newArray = Simplifier.RedundantNots(array.Items);

                    if (Simplifier.HasOnlyLiterals(newArray))
                    {
                        final.AddRange(newArray);
                    }
                    else
                    {
                        final.Add(new PropositionArray(newArray));
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
                                final.Add(lastConnective);
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
                        final.Add(stack.Peek());
                    }

                    stack.Clear();
                    final.Add(member);
                }
            }

            return final;
        }

        public static List<IPropositionMember> MergeOperators(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> final = new List<IPropositionMember>();
            Type membersCommonBinaryConnectiveType = Simplifier.GetCommonBinaryConnectiveType(members);

            foreach (IPropositionMember member in members)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    IList<IPropositionMember> arrayMembers = Simplifier.MergeOperators(array.Items);
                    Type arrayMembersCommonBinaryConnectiveType = Simplifier.GetCommonBinaryConnectiveType(arrayMembers);
                    if (arrayMembersCommonBinaryConnectiveType != null && arrayMembersCommonBinaryConnectiveType == membersCommonBinaryConnectiveType)
                    {
                        final.AddRange(arrayMembers);
                    }
                    else
                    {
                        final.Add(new PropositionArray(arrayMembers));
                    }

                    continue;
                }

                final.Add(member);
            }

            return final;
        }

        public static List<IPropositionMember> RedundantConnectives(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> final = new List<IPropositionMember>();
            List<IPropositionMember> stack = new List<IPropositionMember>();
            Type lastConnectiveType = null;

            foreach (IPropositionMember member in members)
            {
                if (member is PropositionArray)
                {
                    PropositionArray array = member as PropositionArray;

                    IList<IPropositionMember> arrayMembers = Simplifier.RedundantConnectives(array.Items);

                    if (Simplifier.HasOnlyLiterals(arrayMembers))
                    {
                        stack.AddRange(arrayMembers);
                    }
                    else
                    {
                        stack.Add(new PropositionArray(arrayMembers));
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
                                if ((lastItem == null || !(lastItem is UnaryConnectiveBase)) && final.Count > 0)
                                {
                                    final.Add(Activator.CreateInstance(lastConnectiveType) as IPropositionMember);
                                }

                                final.Add(stackMember);
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
                    if ((lastItem == null || !(lastItem is UnaryConnectiveBase)) && final.Count > 0)
                    {
                        final.Add(Activator.CreateInstance(lastConnectiveType) as IPropositionMember);
                    }

                    final.Add(stackMember);
                    lastItem = stackMember;
                }
            }

            return final;
        }

        protected static List<IPropositionMember> RedundantConnectivesStackSimplification(IEnumerable<IPropositionMember> members)
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
