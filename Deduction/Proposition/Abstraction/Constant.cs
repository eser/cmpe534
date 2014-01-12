using System;

namespace Deduction.Proposition.Abstraction
{
    public class Constant : Symbol
    {
        protected readonly bool value;

        public bool Value
        {
            get
            {
                return this.value;
            }
        }

        public Constant(string letter, bool value) : base(letter)
        {
            this.value = value;
        }

        public override bool Equals(object obj)
        {
            Constant constant = obj as Constant;
            if (constant == null)
            {
                return false;
            }

            return (this.Value == constant.Value);
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public object Clone()
        {
            return Activator.CreateInstance(this.GetType(), this.Letter, this.Value);
        }
    }
}
