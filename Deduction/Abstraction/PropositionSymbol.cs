
namespace Deduction.Abstraction
{
    public class PropositionSymbol : IPropositionMemberNegable
    {
        protected readonly char letter;
        protected bool? value;
        protected bool negated;

        public char Letter {
            get
            {
                return this.letter;
            }
        }

        public bool? Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
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

        public PropositionSymbol(char letter, bool? value = null, bool negated = false)
        {
            this.letter = letter;
            this.value = value;
            this.negated = negated;
        }

        public override string ToString()
        {
            if (this.Negated)
            {
                return "!" + this.Letter.ToString();
            }

            return this.Letter.ToString();
        }

        public override bool Equals(object obj)
        {
            PropositionSymbol symbol = obj as PropositionSymbol;
            if (symbol == null)
            {
                return false;
            }

            return (this.Letter == symbol.Letter) && (this.Negated == symbol.Negated);
        }
    }
}
