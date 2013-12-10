#pragma once

#include "../PropositionConstant.h"

namespace DeductionCpp { namespace Abstraction { namespace Constants {

class True : public PropositionConstant
{
protected:

public:
    True(bool negated = false) : PropositionConstant(negated)
    {
    }

    //virtual ~True()
    //{
    //}

    virtual bool GetValue() const
    {
        return true;
    }
};

} } }