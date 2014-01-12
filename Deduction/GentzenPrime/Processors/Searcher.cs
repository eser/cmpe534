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

        public List<Sequent> Expand(Sequent sequent)
        {
            List<Sequent> expanded = new List<Sequent>();
            expanded.Add(new Sequent());

            // replace with tree

            bool skip = false;
            #region apply left rule
            foreach (IMember leftMember in sequent.Left)
            {
                if (!skip)
                {
                    IMember[][] leftMemberApplied = this.ApplyLeftRule(leftMember);

                    if (leftMemberApplied != null)
                    {
                        for (int i = 0; i < leftMemberApplied.Length; i++)
                        {
                            if (expanded.Count <= i)
                            {
                                expanded.Add(expanded[i - 1].Clone() as Sequent);
                            }

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
            foreach (IMember rightMember in sequent.Right)
            {
                if (!skip)
                {
                    IMember[][] rightMemberApplied = this.ApplyRightRule(rightMember);

                    if (rightMemberApplied != null)
                    {
                        for (int i = 0; i < rightMemberApplied.Length; i++)
                        {
                            if (expanded.Count <= i)
                            {
                                expanded.Add(expanded[i - 1].Clone() as Sequent);
                            }

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

            return expanded;
        }

        public IMember[][] ApplyLeftRule(IMember member)
        {
            if (member is And)
            {
                And and = member as And;
                return new IMember[][] { new IMember[] { and.Parameters[0], and.Parameters[1] } };
            }

            return null;
        }

        public IMember[][] ApplyRightRule(IMember member)
        {
            if (member is Or)
            {
                Or or = member as Or;
                return new IMember[][] { new IMember[] { or.Parameters[0], or.Parameters[1] } };
            }

            return null;
        }
    }
}
