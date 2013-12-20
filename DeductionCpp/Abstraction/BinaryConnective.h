#pragma once

#include "IPropositionMember.h"
#include "PropositionMemberTypes.h"

namespace DeductionCpp { namespace Abstraction {

class BinaryConnective : public IPropositionMember
{
protected:

public:
    BinaryConnective() : IPropositionMember()
    {
    }

    //virtual ~BinaryConnective()
    //{
    //}

    virtual PropositionMemberTypes GetType() const
    {
        return PropositionMemberTypes::BinaryConnective;
    }

    virtual bool Operation(bool left, bool right) = 0;
};

} }