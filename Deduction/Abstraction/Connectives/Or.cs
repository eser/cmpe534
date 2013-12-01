
namespace Deduction.Abstraction.Connectives
{
    public class Or : BinaryConnectiveBase
    {
        public override bool Operation(bool left, bool right)
        {
            return left || right;
        }
    }
}
