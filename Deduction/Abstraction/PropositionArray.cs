using System.Collections.Generic;

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
    }
}
