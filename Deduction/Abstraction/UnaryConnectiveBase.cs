
namespace Deduction.Abstraction
{
    public abstract class UnaryConnectiveBase : IConnective
    {
        public abstract bool Operation(bool value);

        public override string ToString()
        {
            DomainMember domainMember = Domain.GetMemberByType(this.GetType());
            return domainMember.SymbolChar.ToString();
        }
    }
}
