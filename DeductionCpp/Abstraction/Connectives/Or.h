#pragma once

#include "../BinaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class Or : public BinaryConnective
{
protected:

public:
    Or() : BinaryConnective()
    {
    }

    //virtual ~Or()
    //{
    //}

    virtual bool Operation(bool left, bool right)
    {
        return left || right;
    }
};

} } }