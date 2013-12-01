using System;
using System.Collections.Generic;
using Deduction.Abstraction.Connectives;

namespace Deduction.Abstraction
{
    public class PropositionArray : IPropositionValue
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
                this.negated = true;
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

        // TODO: implement Equals
        public override bool Equals(object obj)
        {
            return false;
        }

        public bool HasOnlyLiterals()
        {
            foreach (IPropositionMember member in this.items)
            {
                if (!(member is IPropositionValue || member is UnaryConnectiveBase))
                {
                    return false;
                }
            }

            return true;
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
    }
}
