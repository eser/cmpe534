#pragma once

#include "../PropositionConstant.h"

namespace DeductionCpp { namespace Abstraction { namespace Constants {

class False : public PropositionConstant
{
protected:

public:
    False(bool negated = false) : PropositionConstant(negated)
    {
    }

    //virtual ~True()
    //{
    //}

    virtual bool GetValue() const
    {
        return false;
    }
};

} } }