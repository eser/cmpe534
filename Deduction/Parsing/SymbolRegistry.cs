using System;
using System.Collections.Generic;
using Deduction.Abstraction.Connectives;
using Deduction.Abstraction.Constants;

namespace Deduction.Parsing
{
    public class SymbolRegistry
    {
        protected static SymbolRegistry instance = null;
        protected readonly Dictionary<char, Type> connectives;
        protected readonly Dictionary<char, Type> constants;

        public Dictionary<char, Type> Connectives
        {
            get
            {
                return this.connectives;
            }
        }

        public Dictionary<char, Type> Constants
        {
            get
            {
                return this.constants;
            }
        }

        public static SymbolRegistry Instance
        {
            get
            {
                if (SymbolRegistry.instance == null)
                {
                    SymbolRegistry.instance = new SymbolRegistry();
                }

                return SymbolRegistry.instance;
            }
        }

        protected SymbolRegistry()
        {
            this.connectives = new Dictionary<char, Type>()
            {
                { '&', typeof(And) },
                { '|', typeof(Or) },
                { '!', typeof(Not) }
            };

            this.constants = new Dictionary<char, Type>()
            {
                { 't', typeof(True) },
                { 'f', typeof(False) }
            };
        }

        public static char? GetConnectiveSymbol(Type type)
        {
            foreach (KeyValuePair<char, Type> pair in SymbolRegistry.Instance.Connectives)
            {
                if (pair.Value == type)
                {
                    return pair.Key;
                }
            }

            return null;
        }

        public static char? GetConstantSymbol(Type type)
        {
            foreach (KeyValuePair<char, Type> pair in SymbolRegistry.Instance.Constants)
            {
                if (pair.Value == type)
                {
                    return pair.Key;
                }
            }

            return null;
        }
    }
}
