
namespace Deduction.Abstraction.Connectives
{
    public class And : BinaryConnectiveBase
    {
        public override bool Operation(bool left, bool right)
        {
            return left && right;
        }
    }
}
