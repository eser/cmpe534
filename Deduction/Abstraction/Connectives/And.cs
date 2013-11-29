
namespace Deduction.Abstraction.Connectives
{
    public class And : BinaryConnectiveBase
    {
        public override bool Operation(bool first, bool second)
        {
            return first && second;
        }
    }
}
