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
            simplified = Simplifier.RedundantConnectives(simplified);
            simplified = Simplifier.SimplifyConnectives(simplified);

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

        public static List<IPropositionMember> RedundantConnectives(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> final = new List<IPropositionMember>();
            Stack<UnaryConnectiveBase> stack = new Stack<UnaryConnectiveBase>();

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
                    List<IPropositionMember> newArray = Simplifier.RedundantConnectives(array.Items);

                    if (Simplifier.HasOnlyLiterals(newArray))
                    {
                        final.AddRange(newArray);
                    }
                    else
                    {
                        final.Add(new PropositionArray(newArray));
                    }
                }
                else if (member is UnaryConnectiveBase)
                {
                    UnaryConnectiveBase connective = member as UnaryConnectiveBase;

                    if (stack.Count > 0)
                    {
                        UnaryConnectiveBase lastConnective = stack.Peek();

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

        public static List<IPropositionMember> SimplifyConnectives(IEnumerable<IPropositionMember> members)
        {
            List<IPropositionMember> stack = new List<IPropositionMember>();
            List<IPropositionMember> queue = new List<IPropositionMember>(members);
            int pos = 0;

            while (pos < queue.Count)
            {
                IPropositionMember nextInLine = queue[pos];

                if (nextInLine is PropositionArray)
                {
                    PropositionArray array = nextInLine as PropositionArray;
                    List<IPropositionMember> newArray = Simplifier.SimplifyConnectives(array.Items);

                    if (Simplifier.HasOnlyLiterals(newArray))
                    {
                        stack.AddRange(newArray);
                    }
                    else
                    {
                        stack.Add(new PropositionArray(newArray));
                    }
                }
                else if (nextInLine is BinaryConnectiveBase)
                {
                    int lastIndice = stack.Count - 1;
                    IPropositionMember lastItem = stack[lastIndice];
                    IPropositionMember[] nextItemArray = new IPropositionMember[queue.Count - pos - 1];
                    queue.CopyTo(pos + 1, nextItemArray, 0, nextItemArray.Length);

                    BinaryConnectiveBase connective = nextInLine as BinaryConnectiveBase;
                    List<IPropositionMember> simplified = connective.Simplify(lastItem, nextItemArray);

                    if (simplified == null)
                    {
                        stack.Add(connective);
                    }
                    else
                    {
                        stack.RemoveAt(lastIndice);
                        stack.AddRange(simplified);
                    }
                }
                else
                {
                    stack.Add(nextInLine);
                }

                pos++;
            }

            return stack;
        }
    }
}
