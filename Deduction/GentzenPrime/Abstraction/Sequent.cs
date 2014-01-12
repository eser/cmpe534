using System;
using System.Collections.Generic;
using Deduction.Proposition.Abstraction;

namespace Deduction.GentzenPrime.Abstraction
{
    public class Sequent : ICloneable
    {
        public const string SEQUENT_SEPERATOR = "->";
        public const string ITEM_SEPERATOR = ",";

        protected List<IMember> left;
        protected List<IMember> right;

        public Sequent()
        {
            this.left = new List<IMember>();
            this.right = new List<IMember>();
        }

        public List<IMember> Left
        {
            get
            {
                return this.left;
            }
        }

        public List<IMember> Right
        {
            get
            {
                return this.right;
            }
        }

        public bool IsAxiom()
        {
            if (this.Left.Count > 0)
            {
                foreach (IMember leftMember in this.Left)
                {
                    if (leftMember is Constant && (leftMember as Constant).Value == false)
                    {
                        return true;
                    }

                    foreach (IMember rightMember in this.Right)
                    {
                        if (rightMember is Constant && (rightMember as Constant).Value == true)
                        {
                            return true;
                        }

                        if (leftMember.Equals(rightMember))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                foreach (IMember rightMember in this.Right)
                {
                    if (rightMember is Constant && (rightMember as Constant).Value == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsAtomic()
        {
            foreach (IMember leftMember in this.Left)
            {
                if (!leftMember.IsAtomic)
                {
                    return false;
                }
            }

            foreach (IMember rightMember in this.Right)
            {
                if (!rightMember.IsAtomic)
                {
                    return false;
                }
            }

            return true;
        }

        //public void DoOperation(RuleOperation ruleOperation)
        //{
        //    this.DoOperation(ruleOperation.SequentDirection, ruleOperation.SequentOperation, ruleOperation.Member);
        //}

        //public void DoOperation(SequentDirection sequentDirection, SequentOperation sequentOperation, IMember member)
        //{
        //    if (sequentDirection == SequentDirection.Left)
        //    {
        //        switch (sequentOperation)
        //        {
        //            case SequentOperation.Append:
        //                this.Left.Add(member);
        //                break;

        //            case SequentOperation.Prepend:
        //                this.Left.Insert(0, member);
        //                break;
        //        }
        //    }
        //    else if (sequentDirection == SequentDirection.Right)
        //    {
        //        switch (sequentOperation)
        //        {
        //            case SequentOperation.Append:
        //                this.Right.Add(member);
        //                break;

        //            case SequentOperation.Prepend:
        //                this.Right.Insert(0, member);
        //                break;
        //        }
        //    }
        //}

        public object Clone()
        {
            Sequent clone = new Sequent();

            for (int i = 0; i < this.Left.Count; i++)
            {
                clone.Left.Add(this.Left[i].Clone() as IMember);
            }

            for (int i = 0; i < this.Right.Count; i++)
            {
                clone.Right.Add(this.Right[i].Clone() as IMember);
            }

            return clone;
        }
    }
}
