
namespace Deduction.Abstraction
{
    public class PropositionSymbol : IPropositionMember
    {
        public char Letter { get; set; }

        public PropositionSymbol(char letter)
        {
            this.Letter = letter;
        }

        public override bool Equals(object obj)
        {
            PropositionSymbol symbol = obj as PropositionSymbol;
            if (obj == null)
            {
                return false;
            }

            return (this.Letter == symbol.Letter);
        }
    }
}
