using System;
using System.Collections.Generic;
using Deduction.Proposition.Abstraction;

namespace Deduction.GentzenPrime.Abstraction
{
    public class Sequent : ICloneable
    {
        public const string SEQUENT_SEPARATOR = "->";
        public const string ITEM_SEPARATOR = ",";

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
