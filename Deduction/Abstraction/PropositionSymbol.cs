
namespace Deduction.Abstraction
{
    public class PropositionSymbol : IPropositionMember
    {
        public char Letter { get; set; }

        public PropositionSymbol(char letter)
        {
            this.Letter = letter;
        }
    }
}
