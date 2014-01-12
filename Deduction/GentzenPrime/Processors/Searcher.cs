using System.Collections.Generic;
using Deduction.GentzenPrime.Abstraction;
using Deduction.Proposition.Abstraction;
using Deduction.Proposition.Members;
using Deduction.Proposition.Parsing;

namespace Deduction.GentzenPrime.Processors
{
    public class Searcher
    {
        protected readonly Registry registry;

        public Searcher(Registry registry)
        {
            this.registry = registry;
        }

        public void Search(Sequent sequent, Tree<Sequent> tree)
        {
            // consider left side as true if it's empty,
            // consider right side as true if it's empty.
        }

        public void Expand(Tree<Sequent> sequentNode)
        {
            List<Sequent> expanded = new List<Sequent>();
            expanded.Add(new Sequent());

            // replace with tree

            bool skip = false;
            #region apply left rule
            foreach (IMember leftMember in sequentNode.Content.Left)
            {
                if (!skip)
                {
                    IMember[][] leftMemberApplied = this.ApplyLeftRule(leftMember);

                    if (leftMemberApplied != null)
                    {
                        while (expanded.Count < leftMemberApplied.Length)
                        {
                            expanded.Add(expanded[0].Clone() as Sequent);
                        }

                        for (int i = 0; i < leftMemberApplied.Length; i++)
                        {
                            expanded[i].PrependToLeft(leftMemberApplied[i]);
                        }

                        skip = true;
                        continue;
                    }
                }

                foreach (Sequent expandedSequent in expanded)
                {
                    expandedSequent.Left.Add(leftMember);
                }
            }
            #endregion

            #region apply right rule
            foreach (IMember rightMember in sequentNode.Content.Right)
            {
                if (!skip)
                {
                    IMember[][] rightMemberApplied = this.ApplyRightRule(rightMember);

                    if (rightMemberApplied != null)
                    {
                        while (expanded.Count < rightMemberApplied.Length)
                        {
                            expanded.Add(expanded[0].Clone() as Sequent);
                        }

                        for (int i = 0; i < rightMemberApplied.Length; i++)
                        {
                            expanded[i].PrependToRight(rightMemberApplied[i]);
                        }

                        skip = true;
                        continue;
                    }
                }

                foreach (Sequent expandedSequent in expanded)
                {
                    expandedSequent.Right.Add(rightMember);
                }
            }
            #endregion

            if (skip)
            {
                foreach (Sequent expandedSequent in expanded)
                {
                    sequentNode.AddChild(expandedSequent);
                }
            }
        }

        public IMember[][] ApplyLeftRule(IMember member)
        {
            if (member is And)
            {
                And and = member as And;
                return new IMember[][] { new IMember[] { and.Parameters[0], and.Parameters[1] } };
            }

            if (member is Or)
            {
                Or or = member as Or;
                return new IMember[][] { new IMember[] { or.Parameters[0] }, new IMember[] { or.Parameters[1] } };
            }

            return null;
        }

        public IMember[][] ApplyRightRule(IMember member)
        {
            if (member is And)
            {
                And and = member as And;
                return new IMember[][] { new IMember[] { and.Parameters[0] }, new IMember[] { and.Parameters[1] } };
            } 

            if (member is Or)
            {
                Or or = member as Or;
                return new IMember[][] { new IMember[] { or.Parameters[0], or.Parameters[1] } };
            }

            return null;
        }
    }
}
