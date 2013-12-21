#pragma once

#include <functional>
#include "DomainMemberTypes.h"

namespace DeductionCpp { namespace Abstraction {

class IPropositionMember;

class DomainMember
{
protected:

public:
    DomainMemberTypes Type;
    const char SymbolChar;
    int Precedence;
    std::function<IPropositionMember* (DomainMember&)> CreateInstance;

    DomainMember(const char symbolChar, DomainMemberTypes type, int precedence, std::function<IPropositionMember* (DomainMember&)> createInstance) : SymbolChar(symbolChar), Type(type), Precedence(precedence), CreateInstance(createInstance)
    {
    }
};

} }