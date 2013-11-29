using System;
using System.Collections.Generic;
using Deduction.Abstraction.Connectives;

namespace Deduction.Parsing
{
    public class SymbolRegistry
    {
        protected static SymbolRegistry instance = null;
        protected readonly Dictionary<char, Type> connectives;

        public Dictionary<char, Type> Connectives {
            get
            {
                return this.connectives;
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
        }

        public static char? GetSymbol(Type type)
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
    }
}
