
namespace Deduction.Abstraction.Constants
{
    public abstract class ConstantBase : IConstant
    {
        public abstract bool Value { get; }

        public override bool Equals(object obj)
        {
            ConstantBase constant = obj as ConstantBase;
            if (obj == null)
            {
                return false;
            }

            return (this.Value == constant.Value);
        }
    }
}
