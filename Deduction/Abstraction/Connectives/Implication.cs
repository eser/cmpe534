
namespace Deduction.Abstraction.Connectives
{
    public class Implication : BinaryConnectiveBase
    {
        public override bool Operation(bool first, bool second)
        {
            return !(first && !second);
        }
    }
}
