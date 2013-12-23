using System.Collections.Generic;

namespace Deduction.Processors
{
    public class Lexer
    {
        protected readonly string line;
        protected int currentPosition;

        public Lexer(string line)
        {
            this.line = line;
            this.currentPosition = 0;
        }

        public List<string> Analyze()
        {
            List<string> final = new List<string>();
            
            this.Reset();

            while (true) {
                string curr = this.GetNext();
                if (curr == null)
                {
                    break;
                }

                final.Add(curr);
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
