#pragma once

#include "../BinaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class Equivalence : public BinaryConnective
{
protected:

public:
    Equivalence() : BinaryConnective()
    {
    }

    //virtual ~Equivalence()
    //{
    //}

    virtual bool Operation(bool left, bool right)
    {
        return !(left && !right) && !(!left && right);
    }
};

} } }