using System;
using System.Collections.Generic;
using Deduction.Abstraction.DomainMembers;

namespace Deduction.Abstraction
{
    public class PropositionArray : IPropositionMemberNegable
    {
        protected readonly List<IPropositionMember> items;
        protected bool negated;

        public List<IPropositionMember> Items {
            get
            {
                return this.items;
            }
        }

        public bool Negated
        {
            get
            {
                return this.negated;
            }
            set
            {
                this.negated = value;
            }
        }

        public PropositionArray(params IPropositionMember[] items)
        {
            this.items = new List<IPropositionMember>(items);
        }

        public PropositionArray(IEnumerable<IPropositionMember> items)
        {
            this.items = new List<IPropositionMember>(items);
        }

        public override string ToString()
        {
            string final = string.Empty;
            int count = this.Items.Count;

            if (count == 0)
            {
                return final;
            }

            if (this.Negated)
            {
                final += "!";
            }

            if (count > 1)
            {
                final += "(";
            }

            foreach (IPropositionMember member in this.Items)
            {
                final += member.ToString();
            }

            if (count > 1)
            {
                final += ")";
            }

            return final;
        }

        // TODO: implement Equals
        public override bool Equals(object obj)
        {
            return false;
        }

        public Type GetCommonBinaryConnectiveType()
        {
            Type binaryConnectiveType = null;

            foreach (IPropositionMember member in this.items)
            {
                if (!(member is BinaryConnectiveBase))
                {
                    continue;
                }

                if (binaryConnectiveType == null)
                {
                    binaryConnectiveType = member.GetType();
                    continue;
                }

                if (binaryConnectiveType != member.GetType())
                {
                    return null;
                }
            }

            return binaryConnectiveType;
        }

        public bool HasOnlyLiterals()
        {
            foreach (IPropositionMember member in this.items)
            {
                if (!(member is IPropositionMemberNegable || member is UnaryConnectiveBase))
                {
                    return false;
                }
            }

            return true;
        }

        public void AddIntoPropositionArray(PropositionArray target)
        {
            if (this.HasOnlyLiterals())
            {
                target.Items.AddRange(this.items);
                return;
            }

            target.Items.Add(this);
        }
        
        public void AddIntoPropositionArray(ICollection<IPropositionMember> target)
        {
            if (this.HasOnlyLiterals())
            {
                foreach (IPropositionMember member in this.items)
                {
                    target.Add(member);
                }

                return;
            }

            target.Add(this);
        }

        public void AddIntoPropositionArray(Stack<IPropositionMember> target)
        {
            if (this.HasOnlyLiterals())
            {
                foreach (IPropositionMember member in this.items)
                {
                    target.Push(member);
                }

                return;
            }

            target.Push(this);
        }
    }
}
