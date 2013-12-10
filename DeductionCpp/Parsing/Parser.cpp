#include <cctype>
#include "Parser.h"
#include "../Abstraction/PropositionVariable.h"

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
            PropositionVariable* symbol = new PropositionVariable(curr);
            final.Items.push_back(symbol);
        }
        else if (curr == '(')
        {
            string insideParanthesis = this->GetInsideParanthesis();

            Parser insideParser(insideParanthesis);
            PropositionArray* array = new PropositionArray(insideParser.Parse());
            final.Items.push_back(array);
        }
        else
        {
            bool found = false;

            // connectives

            if (!found)
            {
                // constants
            }
        }
    }

    return final;
}

} }