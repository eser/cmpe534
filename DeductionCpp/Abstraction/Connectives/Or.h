#pragma once

#include "../Domain.h"
#include "../BinaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class Or : public BinaryConnective
{
protected:

public:
    static IPropositionMember* CreateInstance(DomainMember& symbolInfo)
    {
        return new Or();
    }

    Or() : BinaryConnective()
    {
    }

    //virtual ~Or()
    //{
    //}

    virtual DomainMember* GetDomainMember()
    {
        return Domain::Instance().GetMemberBySymbolChar('|');
    }

    virtual bool Operation(bool left, bool right)
    {
        return left || right;
    }
};

} } }