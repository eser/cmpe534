
namespace Deduction.Abstraction.Connectives
{
    public class Equivalence : BinaryConnectiveBase
    {
        public override bool Operation(bool left, bool right)
        {
            return !(left && !right) && !(!left && right);
        }
    }
}
