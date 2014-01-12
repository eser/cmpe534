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
            foreach (IMember leftMember in this.Left)
            {
                foreach (IMember rightMember in this.Right)
                {
                    if (leftMember.Equals(rightMember))
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

        public void PrependToLeft(params IMember[] member)
        {
            this.Left.InsertRange(0, member);
        }

        public void PrependToRight(params IMember[] member)
        {
            this.Right.InsertRange(0, member);
        }

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
