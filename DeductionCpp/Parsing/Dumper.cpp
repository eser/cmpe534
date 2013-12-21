#include "Dumper.h"
#include "../Abstraction/DomainMember.h"
#include "../Abstraction/PropositionSymbol.h"

using namespace std;
using namespace DeductionCpp::Abstraction;

namespace DeductionCpp { namespace Parsing {

std::string Dumper::GetString(PropositionArray& input)
{
    string final = "";

    for (auto it = input.Items.begin(); it != input.Items.end(); it++) {
        final += (*it)->ToString();
    }

    return final;
}

} }