using Deduction.Proposition.Abstraction;

namespace Deduction.GentzenPrime.Abstraction
{
    public struct RuleOperation
    {
        private readonly BranchDistribution branchDistribution;
        private readonly SequentDirection sequentDirection;
        private readonly IMember member;
        private readonly bool isModified;

        public RuleOperation(BranchDistribution branchDistribution, SequentDirection sequentDirection, IMember member, bool isModified)
        {
            this.branchDistribution = branchDistribution;
            this.sequentDirection = sequentDirection;
            this.member = member;
            this.isModified = isModified;
        }

        public BranchDistribution BranchDistribution
        {
            get
            {
                return this.branchDistribution;
            }
        }

        public SequentDirection SequentDirection
        {
            get
            {
                return this.sequentDirection;
            }
        }

        public IMember Member
        {
            get
            {
                return this.member;
            }
        }

        public bool IsModified
        {
            get
            {
                return this.isModified;
            }
        }
    }
}
