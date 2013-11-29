
namespace Deduction.Abstraction.Connectives
{
    public class Equivalence : BinaryConnectiveBase
    {
        public override bool Operation(bool first, bool second)
        {
            return !(first && !second) && !(!first && second);
        }
    }
}
