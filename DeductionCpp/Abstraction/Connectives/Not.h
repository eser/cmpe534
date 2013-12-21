#pragma once

#include "../Domain.h"
#include "../UnaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class Not : public UnaryConnective
{
protected:

public:
    static IPropositionMember* CreateInstance(DomainMember& symbolInfo)
    {
        return new Not();
    }

    Not() : UnaryConnective()
    {
    }

    //virtual ~Not()
    //{
    //}

    virtual DomainMember* GetDomainMember()
    {
        return Domain::Instance().GetMemberBySymbolChar('!');
    }

    virtual bool Operation(bool value)
    {
        return !value;
    }
};

} } }