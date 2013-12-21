
namespace Deduction.Abstraction
{
    public interface IPropositionMemberNegable : IPropositionMember
    {
        bool Negated { get; set; }
    }
}
