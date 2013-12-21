#pragma once

#include <string>
#include "IPropositionMember.h"

namespace DeductionCpp { namespace Abstraction {

class IPropositionMemberValuable : public IPropositionMember
{
protected:

public:
    IPropositionMemberValuable() : IPropositionMember()
    {
    }

    //virtual ~IPropositionMemberValuable()
    //{
    //}

    virtual bool GetNegated() const = 0;
    virtual void SetNegated(bool value) = 0;
};

} }