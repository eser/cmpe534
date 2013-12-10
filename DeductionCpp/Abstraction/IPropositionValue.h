#pragma once

#include "IPropositionMember.h"

namespace DeductionCpp { namespace Abstraction {

class IPropositionValue : public IPropositionMember
{
protected:

public:
    IPropositionValue() : IPropositionMember()
    {
    }

    //virtual ~IPropositionValue()
    //{
    //}

    virtual bool GetNegated() const = 0;
    virtual void SetNegated(bool value) = 0;
};

} }