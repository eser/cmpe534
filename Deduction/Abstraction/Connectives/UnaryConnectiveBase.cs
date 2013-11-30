
namespace Deduction.Abstraction.Connectives
{
    public abstract class UnaryConnectiveBase : IConnective
    {
        public abstract bool Operation(bool first);
    }
}
