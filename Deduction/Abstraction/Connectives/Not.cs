
namespace Deduction.Abstraction.Connectives
{
    public class Not : UnaryConnectiveBase
    {
        public override bool Operation(bool first)
        {
            return !first;
        }
    }
}
