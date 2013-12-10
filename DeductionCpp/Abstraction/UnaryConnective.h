#pragma once

#include "IPropositionMember.h"

namespace DeductionCpp { namespace Abstraction {

class UnaryConnective : public IPropositionMember
{
protected:

public:
    UnaryConnective() : IPropositionMember()
    {
    }

    //virtual ~UnaryConnective()
    //{
    //}

    virtual PropositionMemberTypes GetType() const
    {
        return PropositionMemberTypes::UnaryConnective;
    }

    virtual bool Operation(bool value) = 0;
};

} }