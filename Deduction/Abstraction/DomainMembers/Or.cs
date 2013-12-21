
namespace Deduction.Abstraction.DomainMembers
{
    public class Or : BinaryConnectiveBase
    {
        public override bool Operation(bool left, bool right)
        {
            return left || right;
        }
    }
}
