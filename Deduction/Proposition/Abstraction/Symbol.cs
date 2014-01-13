using System;

namespace Deduction.Proposition.Abstraction
{
    public class Symbol : IMember
    {
        protected readonly string letter;

        public Symbol(string letter)
        {
            this.letter = letter;
        }

        public string Letter
        {
            get
            {
                return this.letter;
            }
        }

        public bool IsAtomic
        {
            get
            {
                return true;
            }
        }

        public override bool Equals(object obj)
        {
            Symbol symbol = obj as Symbol;
            if (symbol == null)
            {
                return false;
            }

            return (this.Letter == symbol.Letter);
        }

        public override int GetHashCode()
        {
            return this.letter.GetHashCode();
        }

        public object Clone()
        {
            return Activator.CreateInstance(this.GetType(), this.Letter);
        }
    }
}
