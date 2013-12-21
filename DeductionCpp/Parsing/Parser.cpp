#include <cctype>
#include "Parser.h"
#include "../Abstraction/Domain.h"
#include "../Abstraction/DomainMemberTypes.h"
#include "../Abstraction/PropositionSymbol.h"

using namespace std;
using namespace DeductionCpp::Abstraction;

namespace DeductionCpp { namespace Parsing {

string Parser::GetInsideParanthesis()
{
    int paranthesisCount = 0;
    unsigned int i = this->currentPosition;

    for (; i < this->line.length(); i++)
    {
        if (this->line[i] == '(')
        {
            paranthesisCount++;
            continue;
        }
        else if (this->line[i] == ')')
        {
            paranthesisCount--;
        }

        if (paranthesisCount == 0)
        {
            break;
        }
    }

    if (paranthesisCount == 0)
    {
        string result = this->line.substr(this->currentPosition, i - this->currentPosition);
        this->currentPosition = i;

        return result;
    }

    throw exception("paranthesis count > 0");
}

PropositionArray Parser::Parse()
{
    PropositionArray final;

    while (true)
    {
        char curr = this->GetNext();
        if (curr == NULL)
        {
            break;
        }

        if (isupper(curr))
        {
            PropositionSymbol* symbol = new PropositionSymbol(curr, false);
            final.Items.push_back(symbol);
        }
        else
        {
            DomainMember* member = Domain::Instance().GetMemberBySymbolChar(curr);

            if (member != NULL)
            {
                switch (member->Type)
                {
                    case DomainMemberTypes::Array:
                        {
                            string insideParanthesis = this->GetInsideParanthesis();

                            Parser insideParser(insideParanthesis);
                            PropositionArray* array = new PropositionArray(insideParser.Parse());
                            final.Items.push_back(array);
                        }
                        break;

                    default:
                        IPropositionMember* propositionMember = member->CreateInstance(*member);
                        final.Items.push_back(propositionMember);
                        break;

                    //case DomainMemberTypes::Constant:
                    //    PropositionSymbol* symbol = new PropositionSymbol(curr, true);
                    //    final.Items.push_back(symbol);
                    //    break;

                    //case DomainMemberTypes::UnaryConnective:
                    //    break;

                    //case DomainMemberTypes::BinaryConnective:
                    //    break;
                }
            }
            
        }
    }

    return final;
}

} }