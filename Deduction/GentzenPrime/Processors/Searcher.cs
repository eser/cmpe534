﻿using System.Collections.Generic;
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

        public void Search(Tree<Sequent> tree)
        {
            // consider left side as true if it's empty,
            // consider right side as true if it's empty.

            Queue<Tree<Sequent>> queue = new Queue<Tree<Sequent>>();
            queue.Enqueue(tree);

            while (queue.Count > 0)
            {
                Tree<Sequent> current = queue.Dequeue();

                if (current.Content.IsAxiom())
                {
                    continue;
                }

                this.Expand(current);

                foreach (Tree<Sequent> node in current.Children)
                {
                    queue.Enqueue(node);
                }
            }
        }

        public void Expand(Tree<Sequent> sequentNode)
        {
            List<RuleOperation> ruleOperations = new List<RuleOperation>();
            bool branched = false;

            this.ScanForLeftRules(ref ruleOperations, ref branched, sequentNode.Content.Left);
            this.ScanForRightRules(ref ruleOperations, ref branched, sequentNode.Content.Right);

            Sequent leftBranch = new Sequent();
            Sequent rightBranch = new Sequent();
            int modifiedMembers = 0;
            int branchDiffs = 0;

            foreach (RuleOperation ruleOperation in ruleOperations)
            {
                if (ruleOperation.IsModified)
                {
                    modifiedMembers++;

                    if (ruleOperation.BranchDistribution != BranchDistribution.Both)
                    {
                        branchDiffs++;
                    }
                }

                if (ruleOperation.BranchDistribution == BranchDistribution.Both || ruleOperation.BranchDistribution == BranchDistribution.ToLeft)
                {
                    if (ruleOperation.SequentDirection == SequentDirection.Left)
                    {
                        leftBranch.Left.Add(ruleOperation.Member);
                    }
                    else
                    {
                        leftBranch.Right.Add(ruleOperation.Member);
                    }
                }

                if (ruleOperation.BranchDistribution == BranchDistribution.Both || ruleOperation.BranchDistribution == BranchDistribution.ToRight)
                {
                    if (ruleOperation.SequentDirection == SequentDirection.Left)
                    {
                        rightBranch.Left.Add(ruleOperation.Member);
                    }
                    else
                    {
                        rightBranch.Right.Add(ruleOperation.Member);
                    }
                }
            }

            if (modifiedMembers > 0)
            {
                sequentNode.AddChild(leftBranch);

                if (branchDiffs > 0)
                {
                    sequentNode.AddChild(rightBranch);
                }
            }
        }

        protected void ScanForLeftRules(ref List<RuleOperation> operations, ref bool branched, List<IMember> members)
        {
            foreach (IMember member in members)
            {
                if (member is And)
                {
                    And and = member as And;
                    operations.InsertRange(0,
                        new RuleOperation[]
                        {
                            new RuleOperation(BranchDistribution.Both, SequentDirection.Left, and.Parameters[0], true),
                            new RuleOperation(BranchDistribution.Both, SequentDirection.Left, and.Parameters[1], true)
                        }
                    );
                }
                else if (!branched && member is Or)
                {
                    Or or = member as Or;
                    operations.Insert(0, new RuleOperation(BranchDistribution.ToLeft, SequentDirection.Left, or.Parameters[0], true));
                    operations.Insert(0, new RuleOperation(BranchDistribution.ToRight, SequentDirection.Left, or.Parameters[1], true));

                    branched = true;
                }
                else
                {
                    operations.Add(new RuleOperation(BranchDistribution.Both, SequentDirection.Left, member, false));
                }
            }
        }

        protected void ScanForRightRules(ref List<RuleOperation> operations, ref bool branched, List<IMember> members)
        {
            foreach (IMember member in members)
            {
                if (!branched && member is And)
                {
                    And and = member as And;
                    operations.Insert(0, new RuleOperation(BranchDistribution.ToLeft, SequentDirection.Right, and.Parameters[0], true));
                    operations.Insert(0, new RuleOperation(BranchDistribution.ToRight, SequentDirection.Right, and.Parameters[1], true));

                    branched = true;
                }
                else if (member is Or)
                {
                    Or or = member as Or;
                    operations.InsertRange(0,
                        new RuleOperation[]
                        {
                            new RuleOperation(BranchDistribution.Both, SequentDirection.Right, or.Parameters[0], true),
                            new RuleOperation(BranchDistribution.Both, SequentDirection.Right, or.Parameters[1], true)
                        }
                    );
                }
                else
                {
                    operations.Add(new RuleOperation(BranchDistribution.Both, SequentDirection.Right, member, false));
                }
            }
        }
    }
}
