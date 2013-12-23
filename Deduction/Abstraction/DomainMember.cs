using System;

namespace Deduction.Abstraction
{
    public class DomainMember
    {
        // fields
        protected readonly string symbolChar;
        protected readonly Type type;
        protected readonly int precedence;
        protected readonly bool? value;

        // constructors
        public DomainMember(string symbolChar, Type type, int precedence, bool? value)
        {
            this.symbolChar = symbolChar;
            this.type = type;
            this.precedence = precedence;
            this.value = value;
        }

        // properties
        public string SymbolChar
        {
            get
            {
                return this.symbolChar;
            }
        }

        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        public int Precedence
        {
            get
            {
                return this.precedence;
            }
        }

        public bool? Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
