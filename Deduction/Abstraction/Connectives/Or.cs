
namespace Deduction.Abstraction.Connectives
{
    public class Or : BinaryConnectiveBase
    {
        public override bool Operation(bool first, bool second)
        {
            return first || second;
        }
    }
}
