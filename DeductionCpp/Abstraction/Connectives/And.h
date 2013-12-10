#pragma once

#include "../BinaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class And : public BinaryConnective
{
protected:

public:
    And() : BinaryConnective()
    {
    }

    //virtual ~And()
    //{
    //}

    virtual bool Operation(bool left, bool right)
    {
        return left && right;
    }
};

} } }