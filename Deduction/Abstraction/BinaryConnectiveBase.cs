
namespace Deduction.Abstraction
{
    public abstract class BinaryConnectiveBase : IConnective
    {
        public abstract bool Operation(bool left, bool right);

        public override string ToString()
        {
            DomainMember domainMember = Domain.GetMemberByType(this.GetType());
            return " " + domainMember.SymbolChar + " ";
        }
    }
}
