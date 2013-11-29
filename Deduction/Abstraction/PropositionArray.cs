using System.Collections.Generic;

namespace Deduction.Abstraction
{
    public class PropositionArray : IPropositionMember
    {
        public List<IPropositionMember> Items { get; set; }

        public PropositionArray(params IPropositionMember[] items)
        {
            this.Items = new List<IPropositionMember>(items);
        }

        public PropositionArray(IEnumerable<IPropositionMember> items)
        {
            this.Items = new List<IPropositionMember>(items);
        }
    }
}
