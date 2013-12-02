
namespace Deduction.Abstraction
{
    public class PropositionSymbol : IPropositionValue
    {
        protected readonly char letter;
        protected bool negated;

        public char Letter {
            get
            {
                return this.letter;
            }
        }

        public bool Negated
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

        public PropositionSymbol(char letter, bool negated = false)
        {
            this.letter = letter;
            this.negated = negated;
        }

        public override bool Equals(object obj)
        {
            PropositionSymbol symbol = obj as PropositionSymbol;
            if (obj == null)
            {
                return false;
            }

            return (this.Letter == symbol.Letter) && (this.Negated == symbol.Negated);
        }
    }
}
