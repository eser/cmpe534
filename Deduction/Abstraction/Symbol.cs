﻿
namespace Deduction.Abstraction
{
    public class Symbol : IPropositionMember
    {
        protected readonly string letter;

        public string Letter
        {
            get
            {
                return this.letter;
            }
        }

        public Symbol(string letter)
        {
            this.letter = letter;
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
    }
}
