using System.Collections.Generic;
using Deduction.Abstraction;
using Deduction.Abstraction.Connectives;
using Deduction.Abstraction.Constants;

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
                else if ((member is ConstantBase) && !(member is False || member is True))
                {
                    ConstantBase constant = member as ConstantBase;

                    if (constant.Value)
                    {
                        final.Items.Add(new True() { Negated = constant.Negated });
                    }
                    else
                    {
                        final.Items.Add(new False() { Negated = constant.Negated });
                    }
                }
                else if (member is PropositionSymbol)
                {
                    PropositionSymbol symbol = member as PropositionSymbol;

                    if (values.ContainsKey(symbol.Letter))
                    {
                        if (values[symbol.Letter])
                        {
                            final.Items.Add(new True() { Negated = symbol.Negated });
                        }
                        else
                        {
                            final.Items.Add(new False() { Negated = symbol.Negated });
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

                IPropositionMember left = final.Items[position - 2];
                IPropositionMember middle = final.Items[position - 1];
                IPropositionMember right = final.Items[position];

                // left handside
                bool leftTrue;
                bool leftFalse;

                if (left is True)
                {
                    leftTrue = !(left as True).Negated;
                    leftFalse = !leftTrue;
                }
                else if (left is False)
                {
                    leftTrue = (left as False).Negated;
                    leftFalse = !leftTrue;
                }
                else
                {
                    leftTrue = false;
                    leftFalse = false;
                }

                // right handside
                bool rightTrue;
                bool rightFalse;

                if (right is True)
                {
                    rightTrue = !(right as True).Negated;
                    rightFalse = !rightTrue;
                }
                else if (right is False)
                {
                    rightTrue = (right as False).Negated;
                    rightFalse = !rightTrue;
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
                        final.Items.Add(new False());
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

                IPropositionMember left = final.Items[position - 2];
                IPropositionMember middle = final.Items[position - 1];
                IPropositionMember right = final.Items[position];

                // left handside
                bool leftTrue;
                bool leftFalse;

                if (left is True)
                {
                    leftTrue = !(left as True).Negated;
                    leftFalse = !leftTrue;
                }
                else if (left is False)
                {
                    leftTrue = (left as False).Negated;
                    leftFalse = !leftTrue;
                }
                else
                {
                    leftTrue = false;
                    leftFalse = false;
                }

                // right handside
                bool rightTrue;
                bool rightFalse;

                if (right is True)
                {
                    rightTrue = !(right as True).Negated;
                    rightFalse = !rightTrue;
                }
                else if (right is False)
                {
                    rightTrue = (right as False).Negated;
                    rightFalse = !rightTrue;
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
                        final.Items.Add(new True());
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
