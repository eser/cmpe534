
namespace Deduction.Abstraction.Constants
{
    public abstract class ConstantBase : IConstant
    {
        protected bool negated = false;

        public abstract bool Value {
            get;
        }

        public virtual bool Negated
        {
            get
            {
                return this.negated;
            }
            set
            {
                this.negated = value;
            }
        }

        public override bool Equals(object obj)
        {
            ConstantBase constant = obj as ConstantBase;
            if (obj == null)
            {
                return false;
            }

            return (this.Value == constant.Value) && (this.Negated == constant.Negated);
        }
    }
}
