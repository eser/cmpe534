#pragma once

#include "../Domain.h"
#include "../BinaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class Implication : public BinaryConnective
{
protected:

public:
    static IPropositionMember* CreateInstance(DomainMember& symbolInfo)
    {
        return new Implication();
    }

    Implication() : BinaryConnective()
    {
    }

    //virtual ~Implication()
    //{
    //}

    virtual DomainMember* GetDomainMember()
    {
        return Domain::Instance().GetMemberBySymbolChar('>');
    }

    virtual bool Operation(bool left, bool right)
    {
        return !(left && !right);
    }
};

} } }