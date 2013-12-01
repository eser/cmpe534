
namespace Deduction.Abstraction.Connectives
{
    public class Implication : BinaryConnectiveBase
    {
        public override bool Operation(bool left, bool right)
        {
            return !(left && !right);
        }
    }
}
