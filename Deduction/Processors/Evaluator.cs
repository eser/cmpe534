using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.DomainMembers;

namespace Deduction.Processors
{
    public static class Evaluator
    {
        public static PropositionArray Evaluate(PropositionArray input, Dictionary<char, bool> values)
        {
            PropositionArray simplified;

            simplified = Simplifier.Simplify(input);
            simplified = Evaluator.AssignValues(simplified, values);
            simplified = Evaluator.EvaluateAnds(simplified);
            simplified = Evaluator.EvaluateOrs(simplified);
            simplified = Simplifier.Simplify(simplified);

            return simplified;
        }

        public static PropositionArray AssignValues(PropositionArray input, Dictionary<char, bool> values)
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
                    PropositionArray arrayMembers = Evaluator.AssignValues(array, values);

                    arrayMembers.AddIntoPropositionArray(final);
                }
                else if (member is PropositionSymbol)
                {
                    PropositionSymbol symbol = member as PropositionSymbol;
                    DomainMember domainMember = Domain.GetMemberBySymbolChar(symbol.Letter);

                    if (domainMember == null /* && !domainMember.Value.HasValue */ && values.ContainsKey(symbol.Letter))
                    {
                        if (values[symbol.Letter])
                        {
                            final.Items.Add(new PropositionSymbol('t', true, symbol.Negated));
                        }
                        else
                        {
                            final.Items.Add(new PropositionSymbol('f', false, symbol.Negated));
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

        public static PropositionArray EvaluateAnds(PropositionArray input)
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
                    PropositionArray arrayMembers = Evaluator.EvaluateAnds(array);

                    arrayMembers.AddIntoPropositionArray(final);
                }
                else
                {
                    final.Items.Add(member);
                }

                int position = final.Items.Count - 1;
                if (position < 2)
                {
                    continue;
                }

                PropositionSymbol left = final.Items[position - 2] as PropositionSymbol;
                IPropositionMember middle = final.Items[position - 1];
                PropositionSymbol right = final.Items[position] as PropositionSymbol;

                // left handside
                bool leftTrue;
                bool leftFalse;

                if (left != null && left.Value.HasValue)
                {
                    if (left.Value.Value)
                    {
                        leftTrue = !left.Negated;
                        leftFalse = !leftTrue;
                    }
                    else
                    {
                        leftTrue = left.Negated;
                        leftFalse = !leftTrue;
                    }
                }
                else
                {
                    leftTrue = false;
                    leftFalse = false;
                }

                // right handside
                bool rightTrue;
                bool rightFalse;

                if (right != null && right.Value.HasValue)
                {
                    if (right.Value.Value)
                    {
                        rightTrue = !right.Negated;
                        rightFalse = !rightTrue;
                    }
                    else
                    {
                        rightTrue = right.Negated;
                        rightFalse = !rightTrue;
                    }
                }
                else
                {
                    rightTrue = false;
                    rightFalse = false;
                }

                // and middle
                if (middle is And)
                {
                    And connective = middle as And;

                    if (leftFalse || rightFalse)
                    {
                        final.Items.RemoveRange(position - 2, 3);
                        final.Items.Add(new PropositionSymbol('f', false, false));
                    }
                    else if (leftTrue)
                    {
                        final.Items.RemoveRange(position - 2, 3);
                        final.Items.Add(right);
                    }
                    else if (rightTrue)
                    {
                        final.Items.RemoveRange(position - 2, 3);
                        final.Items.Add(left);
                    }
                }
            }

            return final;
        }

        public static PropositionArray EvaluateOrs(PropositionArray input)
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
                    PropositionArray arrayMembers = Evaluator.EvaluateOrs(array);

                    arrayMembers.AddIntoPropositionArray(final);
                }
                else
                {
                    final.Items.Add(member);
                }

                int position = final.Items.Count - 1;
                if (position < 2)
                {
                    continue;
                }

                PropositionSymbol left = final.Items[position - 2] as PropositionSymbol;
                IPropositionMember middle = final.Items[position - 1];
                PropositionSymbol right = final.Items[position] as PropositionSymbol;

                // left handside
                bool leftTrue;
                bool leftFalse;

                if (left != null && left.Value.HasValue)
                {
                    if (left.Value.Value)
                    {
                        leftTrue = !left.Negated;
                        leftFalse = !leftTrue;
                    }
                    else
                    {
                        leftTrue = left.Negated;
                        leftFalse = !leftTrue;
                    }
                }
                else
                {
                    leftTrue = false;
                    leftFalse = false;
                }

                // right handside
                bool rightTrue;
                bool rightFalse;

                if (right != null && right.Value.HasValue)
                {
                    if (right.Value.Value)
                    {
                        rightTrue = !right.Negated;
                        rightFalse = !rightTrue;
                    }
                    else
                    {
                        rightTrue = right.Negated;
                        rightFalse = !rightTrue;
                    }
                }
                else
                {
                    rightTrue = false;
                    rightFalse = false;
                }

                // and middle
                if (middle is Or)
                {
                    And connective = middle as And;

                    if (leftTrue || rightTrue)
                    {
                        final.Items.RemoveRange(position - 2, 3);
                        final.Items.Add(new PropositionSymbol('t', true, false));
                    }
                    else if (leftFalse)
                    {
                        final.Items.RemoveRange(position - 2, 3);
                        final.Items.Add(right);
                    }
                    else if (rightFalse)
                    {
                        final.Items.RemoveRange(position - 2, 3);
                        final.Items.Add(left);
                    }
                }
            }

            return final;
        }
    }
}
