using System;

namespace Deduction.Proposition.Parsing
{
    public class RegistryMember
    {
        // fields
        protected readonly string symbolChar;
        protected readonly Type type;
        protected readonly int precedence;
        protected readonly int parameters;
        protected readonly bool? value;
        protected readonly string[] aliases;
        protected readonly string closesWith;
        protected readonly bool isRightAssociative;

        // constructors
        public RegistryMember(string symbolChar, Type type, int precedence = 10, int parameters = 0, bool? value = null, string[] aliases = null, string closesWith = null, bool isRightAssociative = false)
        {
            this.symbolChar = symbolChar;
            this.type = type;
            this.precedence = precedence;
            this.parameters = parameters;
            this.value = value;
            this.aliases = aliases;
            this.closesWith = closesWith;
            this.isRightAssociative = isRightAssociative;
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

        public int Parameters
        {
            get
            {
                return this.parameters;
            }
        }

        public bool? Value
        {
            get
            {
                return this.value;
            }
        }

        public string[] Aliases
        {
            get
            {
                return this.aliases;
            }
        }

        public string ClosesWith
        {
            get
            {
                return this.closesWith;
            }
        }

        public bool IsRightAssociative
        {
            get
            {
                return this.isRightAssociative;
            }
        }
    }
}
