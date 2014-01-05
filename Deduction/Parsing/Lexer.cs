using System.Collections.Generic;

namespace Deduction.Parsing
{
    public class Lexer
    {
        protected readonly Registry registry;
        protected readonly string line;
        protected int currentPosition;

        public Lexer(Registry registry, string line)
        {
            this.registry = registry;
            this.line = line;
            this.currentPosition = 0;
        }

        public List<Token> Analyze()
        {
            List<Token> final = new List<Token>();

            this.Reset();

            while (true)
            {
                string curr = this.GetNext();
                if (curr == null)
                {
                    break;
                }

                RegistryMember registryMember = registry.GetMemberBySymbolChar(curr);
                Token token = new Token(curr, registryMember);

                final.Add(token);
            }

            return final;
        }

        protected string GetNext()
        {
            if (this.currentPosition >= this.line.Length)
            {
                return null;
            }

            while (char.IsWhiteSpace(this.line[this.currentPosition]))
            {
                if (++this.currentPosition >= this.line.Length)
                {
                    return null;
                }
            }

            if (char.IsLetterOrDigit(this.line[this.currentPosition]))
            {
                string final = string.Empty;

                do
                {
                    final += this.line[this.currentPosition];

                    if (++this.currentPosition >= this.line.Length)
                    {
                        return final;
                    }
                } while (char.IsLetterOrDigit(this.line[this.currentPosition]));

                return final;
            }

            return this.line[this.currentPosition++].ToString();
        }

        protected void Reset()
        {
            this.currentPosition = 0;
        }
    }
}
