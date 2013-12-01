
namespace Deduction.Abstraction
{
    public interface IPropositionValue : IPropositionMember
    {
        bool Negated { get; }
    }
}
