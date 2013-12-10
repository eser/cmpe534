#pragma once

#include "../BinaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class Implication : public BinaryConnective
{
protected:

public:
    Implication() : BinaryConnective()
    {
    }

    //virtual ~Implication()
    //{
    //}

    virtual bool Operation(bool left, bool right)
    {
        return !(left && !right);
    }
};

} } }