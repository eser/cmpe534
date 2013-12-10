#pragma once

#include "PropositionMemberTypes.h"

namespace DeductionCpp { namespace Abstraction {

class IPropositionMember
{
protected:

public:
    IPropositionMember()
    {
    }

    //virtual ~IPropositionMember()
    //{
    //}

    virtual PropositionMemberTypes GetType() const = 0;
};

} }