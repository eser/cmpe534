
namespace Deduction.Abstraction.DomainMembers
{
    public class Not : UnaryConnectiveBase
    {
        public override bool Operation(bool value)
        {
            return !value;
        }
    }
}
