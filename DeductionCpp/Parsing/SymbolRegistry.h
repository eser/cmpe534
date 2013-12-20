#pragma once

#include <string>
#include <map>
#include "../Abstraction/PropositionMemberTypes.h"

namespace DeductionCpp { namespace Parsing {

class SymbolRegistry
{
protected:
    static SymbolRegistry* SymbolRegistryInstance;

    SymbolRegistry();

    //virtual ~SymbolRegistry()
    //{
    //}

public:
    static SymbolRegistry& Instance();
    static void Dispose();

    static char GetConnectiveSymbol(DeductionCpp::Abstraction::PropositionMemberTypes type);
    static char GetConstantSymbol(DeductionCpp::Abstraction::PropositionMemberTypes type);

    std::map<char, DeductionCpp::Abstraction::PropositionMemberTypes> Connectives;
    std::map<char, DeductionCpp::Abstraction::PropositionMemberTypes> Constants;
};

} }